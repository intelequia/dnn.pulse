namespace Intelequia.Modules.DNNPulse.Model
{

    public class JsonBase
    {
        public Data data { get; set; }
        public string iKey { get; set; }
        public string name { get; set; }
        public string time { get; set; }
    }

    public class Data
    {
        public Basedata baseData { get; set; }
        public string baseType { get; set; }
    }

    public class Basedata
    {
        public string name { get; set; }
        public Properties properties { get; set; }
    }

    public class Properties
    {
        public string DNNVersion { get; set; }
        public string modules { get; set; }
        public string[] portalAliases { get; set; }
    }

    public class Module
    {
        public string moduleName { get; set; }
        public string version { get; set; }
    }

}
