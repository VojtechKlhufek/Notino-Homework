using ServiceApp.Convertors.Interfaces;

namespace ServiceApp.Convertors
{
    public class ConvertorFactory : IConvertorFactory
    {
        private readonly IServiceProvider serviceProvider;
        public ConvertorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public (string, IConvertor) GetConvertor(string type)
        {
            switch (type)
            {
                case "application/xml":
                    return (".xml", (IConvertor)serviceProvider.GetService(typeof(JsonToXml)));
                default:
                    throw new NotImplementedException();
            }
            
        }
    }
}
