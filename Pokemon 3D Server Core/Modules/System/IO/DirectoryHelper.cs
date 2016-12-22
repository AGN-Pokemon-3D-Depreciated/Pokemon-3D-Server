using System.IO;

namespace Modules.System.IO
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// Create directory if it is not exist.
        /// </summary>
        /// <param name="Name">Name of the directory.</param>
        public static void CreateDirectoryIfNotExists(string Name)
        {
            if (!Directory.Exists(PathHelper.GetFullPath(Name)))
            {
                Directory.CreateDirectory(PathHelper.GetFullPath(Name));
            }
        }
    }
}