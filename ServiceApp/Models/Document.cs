namespace ServiceApp.Models
{
    public class Document
    {
        public string Id { get; set; }
        public string[] Tags { get; set; }
        public Object? Data { get; set; }
    }
}
