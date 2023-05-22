using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Scheduling;
using Intelequia.Modules.DNNPulse.Components;
using Intelequia.Modules.DNNPulse.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Intelequia.Modules.DNNPulse.Tasks
{
    public class PulseTask : SchedulerClient
    {
        public PulseTask(ScheduleHistoryItem item) : base()
        {
            this.ScheduleHistoryItem = item;
        }

        /// <summary>
        /// This excecutes everytime the programmed task is called in DNN.
        /// </summary>
        public override void DoWork()
        {
            try
            {
                Model.DNNPulse dnnPulse = DNNPulseManager.GetDNNPulse();
                JsonBase jsonBase = GetMappedJsonBase(dnnPulse);
                Task<string> result = PostToAppInsights(jsonBase);
                // Log a success message.
                this.ScheduleHistoryItem.AddLogNote(result.Result);
                this.ScheduleHistoryItem.Succeeded = true;
            }
            catch (Exception ex)
            {
                // Log an error message.
                this.ScheduleHistoryItem.AddLogNote(ex.Message);
                this.ScheduleHistoryItem.Succeeded = false;
                this.Errored(ref ex);
            }
        }
        /// <summary>
        /// This method makes a POST Request to your Azure Application Insights resource.
        /// </summary>
        /// <param name="jsonBase"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> PostToAppInsights(JsonBase jsonBase)
        {
            string json = JsonConvert.SerializeObject(jsonBase, Formatting.Indented);
            const string URL = "https://dc.services.visualstudio.com/v2/track";
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(URL, content);

            if (response.IsSuccessStatusCode)
            {
                return "POST request succeded";
            }
            else
            {
                throw new Exception("POST request failed: " + response.StatusCode + " - " + response.ReasonPhrase);
            }

        }
        /// <summary>
        /// This method changes the Database stored procedure result into a JSON.
        /// </summary>
        /// <param name="dnnPulse"></param>
        /// <returns></returns>
        public static JsonBase GetMappedJsonBase(Model.DNNPulse dnnPulse)
        {
            const string BASETYPE = "EventData";
            // Here we get the keys from the web.config of DNN.
            string iKey = Config.GetSetting("DNNPulse.Ikey");
            string name = Config.GetSetting("DNNPulse.Name");
            string time = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss tt");
            List<Module> jsonModules = new List<Module>();
            Properties jsonProperties = new Properties
            {
                DNNVersion = dnnPulse.DNNVersion,
                portalAliases = dnnPulse.PortalAlias.ToArray(),
            };
            for (int cont = 0; cont < dnnPulse.ModulesName.Count; cont++)
            {
                Module jsonModule = new Module
                {
                    version = dnnPulse.ModulesVersion[cont],
                    moduleName = dnnPulse.ModulesName[cont]
                };
                jsonModules.Add(jsonModule);
            }
            jsonProperties.modules = JsonConvert.SerializeObject(jsonModules, Formatting.None);
            Basedata jsonBaseData = new Basedata
            {
                properties = jsonProperties,
                name = "DNNPulse"
            };

            Model.Data jsonData = new Model.Data
            {
                baseData = jsonBaseData,
                baseType = BASETYPE
            };

            JsonBase jsonBase = new JsonBase
            {
                data = jsonData,
                iKey = iKey,
                name = name,
                time = time
            };
            return jsonBase;
        }
    }
}
