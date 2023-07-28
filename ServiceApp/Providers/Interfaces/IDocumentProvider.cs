
namespace ServiceApp.Providers.Interfaces
{
    public interface IDocumentProvider
    {
        Task UploadDocument(Models.Document document);
        Task ModifyDocument(Models.Document document);
        Task<(string,Stream)> DownloadDocument(string id, string? accept);
    }
}
