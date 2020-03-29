using System.Configuration;
using System.Reflection;
using powerGateServer.SDK;

namespace DynamicsNav.Plugin
{
    [WebServiceData("coolOrange", "DynamicsNav")]
    public class WebService : powerGateServer.SDK.WebService
    {
        public static readonly string FileStorageLocation;
        public static readonly string NoSeriesCode;

        public WebService()
        {
            AddMethod(new Materials());
            AddMethod(new BomHeaders());
            AddMethod(new BomRows());
            AddMethod(new Documents());
        }

        static WebService()
        {
            var configFullName = Assembly.GetExecutingAssembly().Location + ".config";
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFullName };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var section = configuration.GetSection("DynamicsNAV") as AppSettingsSection;
            if (section == null) return;
            FileStorageLocation = section.Settings["FileStorageLocation"].Value;
            NoSeriesCode = section.Settings["NoSeriesCode"].Value;
        }
    }
}