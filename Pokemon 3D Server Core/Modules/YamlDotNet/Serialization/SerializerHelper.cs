using System;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace Modules.YamlDotNet.Serialization
{
    public static class SerializerHelper
    {
        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="file">A relative or absolute path for the file to save.</param>
        public static bool Serialize(this object obj, string file)
        {
            try
            {
                using (FileStream fileStream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        SerializerBuilder serializer = new SerializerBuilder();
                        serializer.EmitDefaults().Build().Serialize(writer, obj);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="file">A relative or absolute path for the file to save.</param>
        /// <param name="ex">Represents errors that occur during application execution.</param>
        public static bool Serialize(this object obj, string file, out Exception ex)
        {
            try
            {
                using (FileStream fileStream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        SerializerBuilder serializer = new SerializerBuilder();
                        serializer.EmitDefaults().Build().Serialize(writer, obj);
                        ex = null;
                        return true;
                    }
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                return false;
            }
        }
    }
}