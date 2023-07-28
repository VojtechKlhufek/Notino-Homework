using ServiceApp.Convertors;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceAppUnitTests
{
    [TestClass]
    public class JsonToXmlTest
    {
        [TestMethod]
        public void ConvertTest()
        {
            string jsonInput = "{\"Id\": 9,\"Tags\":[\"testtag\",\"testtag2\"],\"Data\":{\"test\":\"test value\"}}";
            

            var convertor = new JsonToXml();

            var result = convertor.Convert(jsonInput);

            Assert.IsTrue(result.Contains("<Id>9</Id>"));
            Assert.IsTrue(result.Contains("<Tags>testtag</Tags>"));
            Assert.IsTrue(result.Contains("<Tags>testtag2</Tags>"));
            Assert.IsTrue(result.Contains("<Data>"));
            Assert.IsTrue(result.Contains("<test>test value</test>"));
            Assert.IsTrue(result.Contains("</Data>"));
        }

        

    }
}