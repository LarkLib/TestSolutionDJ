using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 1. 用来方便实现XML序列化、反序列化、节点等操作
    /// 2. 序列化是指一个对象的实例可以被保存，保存成一个二进制串或者XML等格式字符串。反序列化这是从这些内容中还原为一个对象实例的操作。
    /// 3. 要实现对象的序列化，首先要保证该对象可以序列化。而且，序列化只是将对象的属性进行有效的保存，
    ///    对于对象的一些方法则无法实现序列化的。实现一个类可序列化的最简便的方法就是增加Serializable属性标记类。
    /// 4. DOM(文档对象模型)把层次中的每一个对象都称之为节点（NODE），以HTML超文本标记语言为例：整个文档的一个根就是html,在DOM中可以
    ///    使用document.documentElement来访问它，它就是整个节点树的根节点（ROOT）。 
    /// </summary>
    public class XmlHelper
    {
        protected string strXmlFile;
        protected XmlDocument objXmlDoc = new XmlDocument();
        /// <summary>    
        /// 构造函数    
        /// </summary>    
        /// <param name="XmlFile">XML文件路径</param> 
        public XmlHelper(string XmlFile)
        {
            try
            {
                this.objXmlDoc.Load(XmlFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.strXmlFile = XmlFile;
        }
        /// <summary>    
        /// 二进制序列化    
        /// </summary>    
        /// <param name="path">文件路径</param>    
        /// <param name="obj">对象实例</param>    
        /// <returns></returns> 
        public static bool Serialize(string path, object obj)
        {
            bool result;
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                    stream.Close();
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        /// <summary>    
        /// XML序列化    
        /// </summary>    
        /// <param name="path">文件路径</param>    
        /// <param name="obj">对象实例</param>    
        /// <returns></returns>
        public static bool XmlSerialize(string path, object obj, Type type)
        {
            bool result;
            try
            {
                if (!File.Exists(path))
                {
                    FileInfo fileInfo = new FileInfo(path);
                    if (!fileInfo.Directory.Exists)
                    {
                        Directory.CreateDirectory(fileInfo.Directory.FullName);
                    }
                }
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("", "");
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    xmlSerializer.Serialize(stream, obj, xmlSerializerNamespaces);
                    stream.Close();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }
        /// <summary>    
        /// 二进制反序列化    
        /// </summary>    
        /// <param name="path">文件路径</param>    
        /// <returns></returns>
        public static object Deserialize(string path)
        {
            object result;
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream.Seek(0L, SeekOrigin.Begin);
                    object obj = formatter.Deserialize(stream);
                    stream.Close();
                    result = obj;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }
        /// <summary>    
        /// XML反序列化    
        /// </summary>    
        /// <param name="path">文件路径</param>    
        /// <param name="type">对象类型</param>    
        /// <returns></returns> 
        public static object XmlDeserialize(string path, Type type)
        {
            object result;
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    stream.Seek(0L, SeekOrigin.Begin);
                    object obj = xmlSerializer.Deserialize(stream);
                    stream.Close();
                    result = obj;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }
        /// <summary>    
        /// 获取指定节点下面的XML子节点    
        /// </summary>    
        /// <param name="XmlPathNode">XML节点</param>    
        /// <returns></returns> 
        public XmlNodeList Read(string XmlPathNode)
        {
            XmlNodeList childNodes;
            try
            {
                XmlNode xmlNode = this.objXmlDoc.SelectSingleNode(XmlPathNode);
                childNodes = xmlNode.ChildNodes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return childNodes;
        }
        /// <summary>    
        /// 读取节点属性内容    
        /// </summary>    
        /// <param name="XmlPathNode">XML节点</param>    
        /// <param name="Attrib">节点属性</param>    
        /// <returns></returns>
        public string Read(string XmlPathNode, string Attrib)
        {
            string result = "";
            try
            {
                XmlNode xmlNode = this.objXmlDoc.SelectSingleNode(XmlPathNode);
                result = (Attrib.Equals("") ? xmlNode.InnerText : xmlNode.Attributes[Attrib].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>    
        /// 获取元素节点对象    
        /// </summary>    
        /// <param name="XmlPathNode">XML节点</param>    
        /// <param name="elementName">元素节点名称</param>    
        /// <returns></returns> 
        public XmlElement GetElement(string XmlPathNode, string elementName)
        {
            XmlElement result = null;
            XmlNode xmlNode = this.objXmlDoc.SelectSingleNode(XmlPathNode);
            foreach (XmlNode xmlNode2 in xmlNode)
            {
                XmlElement xmlElement = (XmlElement)xmlNode2;
                if (xmlElement.Name == elementName)
                {
                    result = xmlElement;
                    break;
                }
            }
            return result;
        }
        /// <summary>    
        /// 获取元素节点的值    
        /// </summary>    
        /// <param name="XmlPathNode">XML节点</param>    
        /// <param name="elementName">元素节点名称</param>    
        /// <returns></returns>
        public string GetElementData(string XmlPathNode, string elementName)
        {
            string result = null;
            XmlNode xmlNode = this.objXmlDoc.SelectSingleNode(XmlPathNode);
            foreach (XmlNode xmlNode2 in xmlNode)
            {
                XmlElement xmlElement = (XmlElement)xmlNode2;
                if (xmlElement.Name == elementName)
                {
                    result = xmlElement.InnerText;
                    break;
                }
            }
            return result;
        }
        /// <summary>    
        /// 获取节点下的DataSet    
        /// </summary>    
        /// <param name="XmlPathNode">XML节点</param>    
        /// <returns></returns> 
        public DataSet GetData(string XmlPathNode)
        {
            DataSet dataSet = new DataSet();
            StringReader reader = new StringReader(this.objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
            dataSet.ReadXml(reader);
            return dataSet;
        }
        /// <summary>    
        /// 替换某节点的内容    
        /// </summary>    
        /// <param name="XmlPathNode">XML节点</param>    
        /// <param name="Content">节点内容</param>
        public void Replace(string XmlPathNode, string Content)
        {
            this.objXmlDoc.SelectSingleNode(XmlPathNode).InnerText = Content;
        }
        /// <summary>    
        /// 删除节点    
        /// </summary>    
        /// <param name="Node">节点</param>
        public void Delete(string Node)
        {
            string xpath = Node.Substring(0, Node.LastIndexOf("/"));
            this.objXmlDoc.SelectSingleNode(xpath).RemoveChild(this.objXmlDoc.SelectSingleNode(Node));
        }
        /// <summary>    
        /// 插入一节点和此节点的一子节点    
        /// </summary>    
        /// <param name="MainNode">指定的XML节点</param>    
        /// <param name="ChildNode">指定的XML节点的子节点</param>    
        /// <param name="Element">元素名称</param>    
        /// <param name="Content">内容</param>
        public void InsertNode(string MainNode, string ChildNode, string Element, string Content)
        {
            XmlNode xmlNode = this.objXmlDoc.SelectSingleNode(MainNode);
            XmlElement xmlElement = this.objXmlDoc.CreateElement(ChildNode);
            xmlNode.AppendChild(xmlElement);
            XmlElement xmlElement2 = this.objXmlDoc.CreateElement(Element);
            xmlElement2.InnerText = Content;
            xmlElement.AppendChild(xmlElement2);
        }
        /// <summary>    
        /// 插入一个节点带一个属性     
        /// </summary>    
        /// <param name="MainNode">指定的XML节点</param>    
        /// <param name="Element">元素名称</param>    
        /// <param name="Attrib">属性名称</param>    
        /// <param name="AttribContent">属性值</param>    
        /// <param name="Content">内容</param> 
        public void InsertElement(string MainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            XmlNode xmlNode = this.objXmlDoc.SelectSingleNode(MainNode);
            XmlElement xmlElement = this.objXmlDoc.CreateElement(Element);
            xmlElement.SetAttribute(Attrib, AttribContent);
            xmlElement.InnerText = Content;
            xmlNode.AppendChild(xmlElement);
        }
        /// <summary>    
        /// 插入XML元素    
        /// </summary>    
        /// <param name="MainNode">指定的XML节点</param>    
        /// <param name="Element">元素名称</param>    
        /// <param name="Content">内容</param> 
        public void InsertElement(string MainNode, string Element, string Content)
        {
            XmlNode xmlNode = this.objXmlDoc.SelectSingleNode(MainNode);
            XmlElement xmlElement = this.objXmlDoc.CreateElement(Element);
            xmlElement.InnerText = Content;
            xmlNode.AppendChild(xmlElement);
        }
        /// <summary>    
        /// 保存XML文档    
        /// </summary> 
        public void Save()
        {
            try
            {
                this.objXmlDoc.Save(this.strXmlFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.objXmlDoc = null;
        }
        /// <summary>
        /// XML采用加密的方式序列化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool XmlSerializeEncrypt(string path, object obj, Type type)
        {
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");
            bool result;
            try
            {
                if (!File.Exists(path))
                {
                    FileInfo fileInfo = new FileInfo(path);
                    if (!fileInfo.Directory.Exists)
                    {
                        Directory.CreateDirectory(fileInfo.Directory.FullName);
                    }
                }
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    string input = "";
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(type);
                        xmlSerializer.Serialize(memoryStream, obj, xmlSerializerNamespaces);
                        memoryStream.Seek(0L, SeekOrigin.Begin);
                        input = Encoding.ASCII.GetString(memoryStream.ToArray());
                    }
                    string s = EncodeHelper.EncryptString(input);
                    byte[] bytes = Encoding.Default.GetBytes(s);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// XML反序列化，并解密
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object XmlDeserializeDecrypt(string path, Type type)
        {
            object result;
            try
            {
                string input = File.ReadAllText(path, Encoding.UTF8);
                string s = EncodeHelper.DecryptString(input, true);
                byte[] bytes = Encoding.Default.GetBytes(s);
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    memoryStream.Seek(0L, SeekOrigin.Begin);
                    object obj = xmlSerializer.Deserialize(memoryStream);
                    memoryStream.Close();
                    result = obj;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = null;
            }
            return result;
        }
    }
}
