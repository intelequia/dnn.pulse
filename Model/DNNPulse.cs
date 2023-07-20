using System;
using System.Collections.Generic;

namespace Intelequia.Modules.DNNPulse.Model
{
    public class DNNPulse
    {
        public DNNPulse()
        {
            PortalAlias = new List<string>();
            ModulesName = new List<string>();
            ModulesVersion = new List<string>();
        }
        public string DNNVersion { get; set; }
        public List<String> PortalAlias { get; set; }
        public List<String> ModulesName { get; set; }
        public List<String> ModulesVersion { get; set; }
        public string DatabaseSize { get; set; }
        public string DatabaseTier { get; set; }

    }
}