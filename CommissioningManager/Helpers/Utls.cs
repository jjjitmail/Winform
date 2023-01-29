using System.IO;
using System.Xml.Serialization;

namespace CommissioningManager.Helpers
{
    public static class Utls<T> where T : class
    {
        public static void SaveToXML(T obj, string fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, obj);
                    writer.Flush();
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    File.WriteAllBytes(fileName, ms.ToArray());
                }
            }
        }

        public static T LoadFromXML(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
        }
    }
}