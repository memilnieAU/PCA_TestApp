using System.IO;
using Xamarin.Essentials;

namespace DL
{
    public interface IFileAccess
    {
        string GetCombinePath(string fileName);
    }

    public class FileSystemAccess : IFileAccess
    {
        public string GetCombinePath(string fileName)
        {
            return Path.Combine(FileSystem.AppDataDirectory, fileName);
        }
    }
}