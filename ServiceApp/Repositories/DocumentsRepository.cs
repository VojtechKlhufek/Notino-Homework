using Azure.Storage.Blobs;
using ServiceApp.Exceptions;
using System.Text;

namespace ServiceApp.Repositories
{
    //Azure blob storage reposirory
    public class DocumentsRepository : Interfaces.IDocumentsRepository
    {
        private readonly string storageConnectionString;
        private readonly string storageContainerName;
        public DocumentsRepository(IConfiguration configuration) 
        {
            storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            storageContainerName = configuration.GetValue<string>("BlobContainerName");
           
        }
        public async Task UploadDocument(string fileName, string data)
        {
            var container = new BlobContainerClient(storageConnectionString, storageContainerName);

            var client = container.GetBlobClient(fileName);

            if (!await client.ExistsAsync())
            {
                await using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    await client.UploadAsync(ms);
                }
            }
            else throw new FileUploadException("File with this id already exists");

        }
        public async Task ModifyDocument(string filename, string data)
        {
            var container = new BlobContainerClient(storageConnectionString, storageContainerName);

            var client = container.GetBlobClient(filename);

            if (await client.ExistsAsync())
            {
                await using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    await client.UploadAsync(ms, true);
                }
            }
            else throw new FileNotFoundException("This file does not exist");
            
        }

        public async Task<Stream> DownloadDocument(string id)
        {
            var container = new BlobContainerClient(storageConnectionString, storageContainerName);
            var file = container.GetBlobClient($"{id}.json");
            
            
            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                return data;
            }
            else throw new FileNotFoundException("This file does not exist");
        }
    }
}
