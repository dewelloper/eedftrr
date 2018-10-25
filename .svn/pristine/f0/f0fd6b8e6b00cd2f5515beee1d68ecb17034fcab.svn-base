using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Atlas.Efes.Core.Extensions
{
    public static class SerializationExtensions
    {
        public static string SerializeObject<T>(this T instance)
        {
            XmlSerializer xmls = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = new UTF8Encoding(false, true);
                settings.Indent = true;
                settings.NewLineChars = Environment.NewLine;
                settings.ConformanceLevel = ConformanceLevel.Document;

                using (XmlWriter writer = XmlTextWriter.Create(ms, settings))
                {
                    xmls.Serialize(writer, instance);
                }

                string xml = Encoding.UTF8.GetString(ms.ToArray());
                return xml;
            }

        }
    }
}
