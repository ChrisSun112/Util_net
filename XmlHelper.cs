using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace Util
{
    public class XmlHelper
    {
        /// <summary>
        /// XML String 序列化对象成字符串
        /// </summary>
        public static string XmlSerialize<T>(T obj)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                //XmlWriterSettings settings = new XmlWriterSettings();
                //settings.OmitXmlDeclaration = true;
                //XmlWriter xmlWriter = XmlWriter.Create(ms, settings);

                xmlSerializer.Serialize(ms, obj, ns);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xmlString;
        }


      
        /// <summary>
        /// XML String 反序列化成对象
        /// </summary>
        public static T XmlDeserialize<T>(string xmlString)
        {
            T t = default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    Object obj = xmlSerializer.Deserialize(xmlReader);
                    t = (T)obj;
                }
            }
            return t;
        }

        
       
    }
}
