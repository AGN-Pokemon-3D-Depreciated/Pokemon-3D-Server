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
        /// <param name="file">A relative or absolute path for the file to load.</param>
        public static T Deserialize<T>(string file) where T : class
        {
            try
            {
                using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        Deserializer deserializer = new Deserializer();
                        return deserializer.Deserialize<T>(reader);
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
        /// <param name="file">A relative or absolute path for the file to load.</param>
        /// <param name="ex">Represents errors that occur during application execution.</param>
        public static T Deserialize<T>(string file, out Exception ex) where T : class
        {
            try
            {
                using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        Deserializer deserializer = new Deserializer();
                        ex = null;
                        return deserializer.Deserialize<T>(reader);
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