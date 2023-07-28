using Microsoft.AspNetCore.Mvc;
using ServiceApp.Providers.Interfaces;
using ServiceApp.Exceptions;

namespace ServiceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentProvider _documentProvider;
        public DocumentsController(IDocumentProvider documentProvider)
        {
            _documentProvider = documentProvider;   
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(Models.Document document)
        {
            try
            {
                await _documentProvider.UploadDocument(document);
                return Ok();
            }
            catch (FileUploadException) 
            {
                return StatusCode(400);
            }
            catch (Exception) 
            {
                return StatusCode(500);
            }
            
            
        }

        [HttpPut("modify")]
        public async Task<IActionResult> Modify(Models.Document document)
        {
            try
            {
                await _documentProvider.ModifyDocument(document);
                return Ok();
            }
            catch(FileNotFoundException) 
            { 
                return StatusCode(404);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(string id)
        {
            try
            {
                var accept = Request?.Headers?.Accept;
                var file = await _documentProvider.DownloadDocument(id, accept);

                return new FileStreamResult(file.Item2, "application/json")
                {
                    FileDownloadName = (file.Item1)
                };
            }
            catch (FileNotFoundException)
            {
                return StatusCode(404);
            }
            catch (NotImplementedException)
            {
                return StatusCode(415);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }



        }
    }
}
