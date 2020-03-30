using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Configuration;
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
            AddMethod(new BomHeaders());
            AddMethod(new BomRows());
            AddMethod(new Categories());
            AddMethod(new Documents());
            AddMethod(new Materials());
            AddMethod(new UnitsOfMeasure());
            AddMethod(new Vendors());
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


        public static System.ServiceModel.Description.ServiceEndpoint GetServiceEndpoint<T>() where T : IClientChannel
        {
            var configFullName = Assembly.GetExecutingAssembly().Location + ".config";
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap { ExeConfigFilename = configFullName }, ConfigurationUserLevel.None);

            var channelType = typeof(T);
            var contractType = channelType.GetInterfaces().First(i => i.Namespace == channelType.Namespace);
            var contractAttribute = contractType.GetCustomAttributes(typeof(ServiceContractAttribute), false).First() as ServiceContractAttribute;

            var serviceModelSectionGroup = ServiceModelSectionGroup.GetSectionGroup(configuration);
            var channelEndpointElement = serviceModelSectionGroup?.Client.Endpoints.OfType<ChannelEndpointElement>().First(e => e.Contract == contractAttribute?.ConfigurationName);
            var channelFactory = new ConfigurationChannelFactory<T>(channelEndpointElement.Name, configuration, null);

            return channelFactory.Endpoint;
        }
    }
}