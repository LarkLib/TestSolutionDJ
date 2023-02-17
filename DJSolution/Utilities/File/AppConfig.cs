using System;
using System.IO;
using System.Xml;

namespace DJ.LMS.Utilities
{
    public sealed class AppConfig
    {
        private string configPath = string.Empty;
        public AppConfig()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App.Config");
            string path2 = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", "");
            if (File.Exists(path))
            {
                this.configPath = path;
            }
            else
            {
                if (!File.Exists(path2))
                {
                    throw new ArgumentNullException("没有找到应用程序配置文件, 请指定配置文件");
                }
                this.configPath = path2;
            }
        }
        public AppConfig(string configFilePath)
        {
            this.configPath = configFilePath;
        }
        public void SetKeyValue(string keyName, string keyValue)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(this.configPath);
                XmlNode xmlNode = xmlDocument.SelectSingleNode("//appSettings");
                XmlElement xmlElement = (XmlElement)xmlNode.SelectSingleNode("//add[@key='" + keyName + "']");
                if (xmlElement != null)
                {
                    xmlElement.SetAttribute("value", keyValue);
                }
                else
                {
                    xmlElement = xmlDocument.CreateElement("add");
                    xmlElement.SetAttribute("key", keyName);
                    xmlElement.SetAttribute("value", keyValue);
                    xmlNode.AppendChild(xmlElement);
                }
                xmlDocument.Save(this.configPath);
            }
            catch { throw new Exception(); }
        }
        public string GetKeyValue(string keyName)
        {
            string result = string.Empty;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(this.configPath);
                XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("add");
                for (int i = 0; i < elementsByTagName.Count; i++)
                {
                    XmlAttribute xmlAttribute = elementsByTagName[i].Attributes["key"];
                    if (xmlAttribute != null && xmlAttribute.Value == keyName)
                    {
                        xmlAttribute = elementsByTagName[i].Attributes["value"];
                        if (xmlAttribute != null)
                        {
                            result = xmlAttribute.Value;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
            return result;
        }
        public string GetSubValue(string keyName, string subKeyName)
        {
            string text = this.GetKeyValue(keyName).ToLower();
            string[] array = text.Split(new char[]
			{
				';'
			});
            string result;
            for (int i = 0; i < array.Length; i++)
            {
                string text2 = array[i].ToLower();
                if (text2.IndexOf(subKeyName.ToLower()) >= 0)
                {
                    int num = array[i].IndexOf("=");
                    result = array[i].Substring(num + 1);
                    return result;
                }
            }
            result = string.Empty;
            return result;
        }
    }
}
