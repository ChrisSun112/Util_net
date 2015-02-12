using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Xsl;
using System.Xml;
using System.Xml.Serialization;

namespace Util
{
    /// <summary>
    /// xslt文件模板转html heler class
    /// </summary>
    public class XsltHelper
    {

        public static string Transform(string xsltPath, Dictionary<string, string> argumentDic)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(xsltPath);
            XsltArgumentList arglist = new XsltArgumentList();
            foreach (var d in argumentDic)
            {
                arglist.AddParam(d.Key, string.Empty, d.Value);
            }

            using (XmlReader xml = XmlReader.Create(new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root></root>")))
            using (MemoryStream reader = new MemoryStream())
            {
                transform.Transform(xml, arglist, reader);
                return Encoding.UTF8.GetString(reader.ToArray());

            }

        }

        /// <summary>
        /// xslt文件转html,支持html table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xsltPath"></param>
        /// <param name="argumentDic"></param>
        /// <param name="tableArgs">html table数据</param>
        /// <returns></returns>
        public static string Transform<T>(string xsltPath, Dictionary<string, string> argumentDic, List<T> tableArgs)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(xsltPath);
            XsltArgumentList arglist = new XsltArgumentList();
            foreach (var d in argumentDic)
            {
                arglist.AddParam(d.Key, string.Empty, d.Value);
            }

            MemoryStream sm = new MemoryStream(XmlSerialize(tableArgs));

            using (XmlReader xml = XmlReader.Create(sm))
            using (MemoryStream reader = new MemoryStream())
            {              
                transform.Transform(xml, arglist, reader);
                sm.Close();
                return Encoding.UTF8.GetString(reader.ToArray());
            }

        }


        public static byte[] XmlSerialize<T>(T obj)
        {

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute("root"));
            using (MemoryStream ms = new MemoryStream())
            {

                xmlSerializer.Serialize(ms, obj, ns);
                return ms.ToArray();
            }

        }
    }
}
