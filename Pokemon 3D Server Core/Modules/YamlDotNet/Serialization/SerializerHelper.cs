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
        /// <param name="Object">The object to serialize.</param>
        /// <param name="File">A relative or absolute path for the file to save.</param>
        public static bool Serialize(this object Object, string File)
        {
            try
            {
                using (FileStream FileStream = new FileStream(File, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter Writer = new StreamWriter(FileStream, Encoding.UTF8))
                    {
                        SerializerBuilder Serializer = new SerializerBuilder();
                        Serializer.EmitDefaults().Build().Serialize(Writer, Object);
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
        /// <param name="Object">The object to serialize.</param>
        /// <param name="File">A relative or absolute path for the file to save.</param>
        /// <param name="ex">Represents errors that occur during application execution.</param>
        public static bool Serialize(this object Object, string File, out Exception ex)
        {
            try
            {
                using (FileStream FileStream = new FileStream(File, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter Writer = new StreamWriter(FileStream, Encoding.UTF8))
                    {
                        SerializerBuilder Serializer = new SerializerBuilder();
                        Serializer.EmitDefaults().Build().Serialize(Writer, Object);
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