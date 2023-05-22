using Intelequia.Modules.DNNPulse.Data;

namespace Intelequia.Modules.DNNPulse.Components
{
    public class DNNPulseManager
    {
        public static Model.DNNPulse GetDNNPulse()
        {

            return DataProvider.Instance().GetDNNPulse();
        }
    }
}