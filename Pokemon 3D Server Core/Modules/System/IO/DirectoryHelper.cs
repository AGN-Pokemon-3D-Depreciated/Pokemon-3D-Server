using System.IO;

namespace Modules.System.IO
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// Create directory if it is not exist.
        /// </summary>
        /// <param name="name">Name of the directory.</param>
        public static void CreateDirectoryIfNotExists(string name)
        {
            if (!Directory.Exists(PathHelper.GetFullPath(name)))
                Directory.CreateDirectory(PathHelper.GetFullPath(name));
        }
    }
}