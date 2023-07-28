using ServiceApp.Convertors.Interfaces;
using ServiceApp.Providers.Interfaces;
using ServiceApp.Repositories.Interfaces;
using System.Text.Json;

namespace ServiceApp.Providers
{
    public class DocumentProvider : IDocumentProvider
    {
        private readonly IDocumentsRepository documentsRepository;
        private readonly IConvertorFactory convertorFactory;
        public DocumentProvider(IDocumentsRepository documentsRepository, IConvertorFactory convertorFactory) 
        {
            this.documentsRepository = documentsRepository;
            this.convertorFactory = convertorFactory;

        }
        public async Task UploadDocument(Models.Document document) 
        {
            var fileName = document.Id.ToString()+".json";
            var jsonString = JsonSerializer.Serialize<Models.Document>(document);
            

            await documentsRepository.UploadDocument(fileName, jsonString);
        }

        public async Task ModifyDocument(Models.Document document)
        {
            var fileName = document.Id.ToString() + ".json";
            var jsonString = JsonSerializer.Serialize<Models.Document>(document);

            await documentsRepository.ModifyDocument(fileName, jsonString);
        }

        public async Task<(string, Stream)> DownloadDocument(string id, string accept)
        {
            var doc = await documentsRepository.DownloadDocument(id);

            if (accept == "application/json")
            {
                return ($"{id}.json", doc);
            }

            var jsonDoc = JsonSerializer.Deserialize<Models.Document>(doc);
            var jsonString = JsonSerializer.Serialize<Models.Document>(jsonDoc);
            doc.Position = 0;

            

            var (fileExtension, convertor) = convertorFactory.GetConvertor(accept);
            var convertedString = convertor.Convert(jsonString);
            var filename = $"{id}.{fileExtension}";

            

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(convertedString);
            writer.Flush();
            stream.Position = 0;

            return (filename, stream);
            
            

           
        }
    }
}
