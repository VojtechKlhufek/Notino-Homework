using ServiceApp.Convertors.Interfaces;
using System.Xml.Linq;

namespace ServiceApp.Convertors
{
    public class JsonToXml : IConvertor
    {
        public string Convert(string document)
        {
            var node = Newtonsoft.Json.JsonConvert.DeserializeXNode(document, "Document");
            return node.ToString();
    
        }
    }
}
