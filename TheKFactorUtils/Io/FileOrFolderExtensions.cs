using System.IO;
using System.Linq;

namespace TheKFactorUtils.Io
{
    public static class FileOrFolderExtensions
    {
        public static bool IsDirectory(this string fileOrFolder)
        {
            return File.GetAttributes(fileOrFolder) == FileAttributes.Directory;
        }

        public static bool IsFile(this string fileOrFolder)
        {
            return !IsDirectory(fileOrFolder);
        }

        public static string GetFileName(this string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public static string GetFileExtension(this string filePath)
        {
            return Path.GetExtension(filePath);
        }

        public static string GetDirPath(this string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        public static string GetCurrentDir(this string dirPath)
        {
            return dirPath.Split('\\').Last();
        }
    }
}
