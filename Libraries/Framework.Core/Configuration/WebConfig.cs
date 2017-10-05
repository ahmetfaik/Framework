using System;
using System.Xml;

namespace Framework.Core.Configuration
{
    public class WebConfig : BaseConfig<WebConfig>
    {
        public override WebConfig Create(XmlNode section)
        {
            Config = new WebConfig();

            var startupNode = section.SelectSingleNode("Startup");
            Config.IgnoreStartupTasks = GetBool(startupNode, "IgnoreStartupTasks");

            return Config;
        }

        #region Properties

        public bool IgnoreStartupTasks { get; set; }

        #endregion

        #region Utilities

        private string GetString(XmlNode node, string attrName)
        {
            return SetByXElement<string>(node, attrName, Convert.ToString);
        }

        private bool GetBool(XmlNode node, string attrName)
        {
            return SetByXElement<bool>(node, attrName, Convert.ToBoolean);
        }

        private T SetByXElement<T>(XmlNode node, string attrName, Func<string, T> converter)
        {
            if (node?.Attributes == null) return default(T);
            var attr = node.Attributes[attrName];
            if (attr == null) return default(T);
            var attrVal = attr.Value;
            return converter(attrVal);
        }

        #endregion

    }
}
