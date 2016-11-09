using System.Collections.Generic;

namespace WebApplication5.Interfaces
{
    public interface IFileService
    {
        IEnumerable<string> GetAllFiles();
    }
}