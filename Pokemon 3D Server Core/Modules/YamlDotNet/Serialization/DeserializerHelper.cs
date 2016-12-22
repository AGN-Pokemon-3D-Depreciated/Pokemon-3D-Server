using System;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace Modules.YamlDotNet.Serialization
{
    public static class DeserializerHelper
    {
        /// <summary>
        /// Deserializes an object of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="File">A relative or absolute path for the file to load.</param>
        public static T Deserialize<T>(string File) where T : class
        {
            try
            {
                using (FileStream FileStream = new FileStream(File, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader Reader = new StreamReader(FileStream, Encoding.UTF8))
                    {
                        Deserializer Deserializer = new Deserializer();
                        return Deserializer.Deserialize<T>(Reader);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Deserializes an object of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="File">A relative or absolute path for the file to load.</param>
        /// <param name="ex">Represents errors that occur during application execution.</param>
        public static T Deserialize<T>(string File, out Exception ex) where T : class
        {
            try
            {
                using (FileStream FileStream = new FileStream(File, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader Reader = new StreamReader(FileStream, Encoding.UTF8))
                    {
                        Deserializer Deserializer = new Deserializer();
                        ex = null;
                        return Deserializer.Deserialize<T>(Reader);
                    }
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                return null;
            }
        }
    }
}