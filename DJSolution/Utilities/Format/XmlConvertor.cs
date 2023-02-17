using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DJ.LMS.Utilities
{
    public sealed class XmlConvertor
    {
        private XmlConvertor()
        {
        }
        public static object XmlToObject(string xml, Type type)
        {
            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }
            object result = null;
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            StringReader input = new StringReader(xml);
            XmlReader xmlReader = new XmlTextReader(input);
            try
            {
                result = xmlSerializer.Deserialize(xmlReader);
            }
            catch (InvalidOperationException innerException)
            {
                throw new InvalidOperationException("Can not convert xml to object", innerException);
            }
            finally
            {
                xmlReader.Close();
            }
            return result;
        }
        public static string ObjectToXml(object obj, bool toBeIndented)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }
            UTF8Encoding uTF8Encoding = new UTF8Encoding(false);
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, uTF8Encoding);
            xmlTextWriter.Formatting = (toBeIndented ? Formatting.Indented : Formatting.None);
            try
            {
                xmlSerializer.Serialize(xmlTextWriter, obj);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Can not convert object to xml.");
            }
            finally
            {
                xmlTextWriter.Close();
            }
            return uTF8Encoding.GetString(memoryStream.ToArray());
        }
    }
}
