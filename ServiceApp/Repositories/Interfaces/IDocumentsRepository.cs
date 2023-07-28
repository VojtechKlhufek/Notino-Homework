namespace ServiceApp.Repositories.Interfaces
{
    public interface IDocumentsRepository
    {

        Task UploadDocument(string fileName, string data);
        Task ModifyDocument(string fileName, string data);
        Task<Stream> DownloadDocument(string id);
    }
}
