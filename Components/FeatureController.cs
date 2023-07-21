using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Scheduling;
using System;
using System.Linq;

namespace Intelequia.Modules.DNNPulse.Components
{
    public class FeatureController : IUpgradeable
    {
        public string UpgradeModule(string version)
        {
            try
            {
                var task = DotNetNuke.Services.Scheduling.SchedulingController.GetSchedule().FirstOrDefault(x => x.TypeFullName == "Intelequia.Modules.DNNPulse.Tasks.PulseTask, DNNPulse");
                if (task != null)
                {
                    SchedulingController.DeleteSchedule(task.ScheduleID);
                }

                string fullName = "Intelequia.Modules.DNNPulse.Tasks.PulseTask, DNNPulse";
                var startTime = DateTime.Now;
                var timeLapse = 1;
                var timeLapseMeasurement = "d";
                var retryTimeLapse = 30;
                var retryTimeLapseMeasurement = "m";
                var retainHistoryNum = 0;
                var attatchToEvent = "";
                var catchUpEnable = false;
                var enable = true;
                var objectDependency = "";
                var servers = "";
                var friendlyName = "DNN Pulse";
                // Add the scheduled task
                SchedulingController.AddSchedule(
    fullName, timeLapse, timeLapseMeasurement, retryTimeLapse, retryTimeLapseMeasurement, retainHistoryNum, attatchToEvent, catchUpEnable, enable, objectDependency, servers, friendlyName, startTime);

                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed: " + ex.Message;
            }
        }

    }
}