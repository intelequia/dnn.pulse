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
                var fullName = "Intelequia.Modules.DNNPulse.Tasks.PulseTask, DNNPulse";
                var startTime = DateTime.Now;
                var timeLapse = 1;
                var timeLapseMeasurement = "d";
                var retryTimeLapse = 30;
                var retryTimeLapseMeasurement = "m";
                var retainHistoryNum = 0;
                var attachToEvent = "";
                var catchUpEnabled = false;
                var enabled = true;
                var objectDependencies = "";
                var servers = "";
                var friendlyName = "DNN Pulse";

                var task = DotNetNuke.Services.Scheduling.SchedulingController.GetSchedule()
                    .FirstOrDefault(x => x.TypeFullName == fullName);

                task = task ?? new ScheduleItem();

                task.ScheduleStartDate = startTime;
                task.TimeLapse = timeLapse;
                task.TimeLapseMeasurement = timeLapseMeasurement;
                task.RetryTimeLapse = retryTimeLapse;
                task.RetainHistoryNum = retainHistoryNum;
                task.AttachToEvent = attachToEvent;
                task.CatchUpEnabled = catchUpEnabled;
                task.Enabled = enabled;
                task.ObjectDependencies = objectDependencies;
                task.Servers = servers;
                task.FriendlyName = friendlyName;

                if (task.ScheduleID > 0)
                {
                    SchedulingController.UpdateSchedule(task);
                }
                else
                {
                    // Add the scheduled task
                    SchedulingController.AddSchedule(
                        fullName, timeLapse, timeLapseMeasurement, retryTimeLapse, retryTimeLapseMeasurement,
                        retainHistoryNum, attachToEvent, catchUpEnabled, enabled, objectDependencies, servers,
                        friendlyName, startTime);
                }


                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed: " + ex.Message;
            }
        }

    }
}