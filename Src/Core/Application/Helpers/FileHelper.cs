using System.Collections.Generic;
using System.IO;

namespace Application.Helpers
{
    public static class FileHelper
    {
        private static string GetRootPath(string path)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string rootPath = currentDirectory + path;
            return rootPath;
        }

        public static string GetFileNameWithExt(string path, string fileName, string defaultFileName)
        {
            string fileNameWithExt = string.Empty;
            try
            {
                string rootPath = GetRootPath(path);
                string[] files = Directory.GetFiles(rootPath, fileName + ".*");

                if (files.Length == 0 && !string.IsNullOrWhiteSpace(defaultFileName))
                    files = Directory.GetFiles(rootPath, defaultFileName + ".*");

                if (files.Length > 0) fileNameWithExt = Path.GetFileName(files[0]);
            }
            catch { }
            return fileNameWithExt;
        }

        public static IReadOnlyList<string> GetFileNamesWithExt(string path)
        {
            List<string> fileNamesWithExt = new List<string>();
            try
            {
                string rootPath = GetRootPath(path);
                string[] files = Directory.GetFiles(rootPath);
                foreach(string file in files)
                {
                    string fileNameWithExt = Path.GetFileName(file);
                    fileNamesWithExt.Add(fileNameWithExt);
                }
            }
            catch { }
            return fileNamesWithExt;
        }
    }
}