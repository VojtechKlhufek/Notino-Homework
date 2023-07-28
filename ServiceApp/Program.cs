using ServiceApp.Providers;
using ServiceApp.Providers.Interfaces;
using ServiceApp.Repositories;
using ServiceApp.Repositories.Interfaces;
using ServiceApp.Convertors;
using ServiceApp.Convertors.Interfaces;


namespace ServiceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });

            builder.Services.AddScoped<IDocumentProvider, DocumentProvider>();

            builder.Services.AddScoped<IDocumentsRepository, DocumentsRepository>();

            builder.Services.AddScoped<IConvertorFactory, ConvertorFactory>();

            builder.Services.AddScoped<JsonToXml>()
                .AddScoped<IConvertor, JsonToXml>(s => s.GetService<JsonToXml>());


            var app = builder.Build();
            
            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}