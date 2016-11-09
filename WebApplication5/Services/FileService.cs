using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication5.Interfaces;

namespace WebApplication5.Services
{
    public class FileService : IFileService
    {
        public IEnumerable<string> GetAllFiles()
        {
            // Change this path to match your directory containing the files you want to expose
            var files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/allfiles"), "*.*", SearchOption.AllDirectories);
            var fileNames = files.Select(Path.GetFileName);
            return fileNames;
        }
    }
}