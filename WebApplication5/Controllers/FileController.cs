using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebApplication5.Interfaces;

namespace WebApplication5.Controllers
{
    [RoutePrefix("files")]
    public class FileController : ApiController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [Route("")]
        public HttpResponseMessage GetFile([FromUri] string filename = null)
        {
            if (filename == null)
            {
                var fileNames = _fileService.GetAllFiles();
                return Request.CreateResponse(HttpStatusCode.OK, fileNames);
            }
            var path = System.Web.HttpContext.Current.Server.MapPath("~/allfiles/" + filename);

            if (!File.Exists(path))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(filename));
            result.Content.Headers.ContentLength = stream.Length;
            return result;

        }
    }
}
