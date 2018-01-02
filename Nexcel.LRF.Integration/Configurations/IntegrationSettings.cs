using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;

namespace Nexcel.LRF.Integration.Configurations
{
    public class IntegrationSettings : ConfigurationSection, IIntegrationSettings
    {
        private static readonly Regex UrlRegularExpression = new Regex("(http|https):\\/\\/([\\w@][\\w.:@]+)\\/?[\\w\\.?=%&=\\-@/$,]*");
        private static IntegrationSettings settings = ConfigurationManager.GetSection("Nexcel.LRF.Integration") as IntegrationSettings;

        public static IntegrationSettings Settings
        {
            get
            {
                return IntegrationSettings.settings;
            }
        }

        [ConfigurationProperty("PublicKeyPath", IsRequired = true)]
        public string PublicKeyPath
        {
            get
            {
                if (object.ReferenceEquals(IntegrationSettings.settings["PublicKeyPath"], null) || string.IsNullOrEmpty(IntegrationSettings.settings["PublicKeyPath"].ToString()))
                {
                    throw new ConfigurationErrorsException(string.Format(Resources.ConfigurationRequired, "PublicKeyPath"));
                }

                var text = IntegrationSettings.settings["PublicKeyPath"].ToString();
                if (text.StartsWith("~/"))
                {
                    if (HttpContext.Current == null)
                    {
                        throw new NullReferenceException(Resources.HttpContextNullException);
                    }

                    text = HttpContext.Current.Server.MapPath(text);
                }

                return text;
            }

            set
            {
                IntegrationSettings.settings["PublicKeyPath"] = value;
            }
        }

        [ConfigurationProperty("SsoUrl", IsRequired = true)]
        public string SsoUrl
        {
            get
            {
                if (object.ReferenceEquals(IntegrationSettings.settings["SsoUrl"], null) || string.IsNullOrEmpty(IntegrationSettings.settings["SsoUrl"].ToString()))
                {
                    throw new ConfigurationErrorsException(string.Format(Resources.ConfigurationRequired, "SsoUrl"));
                }

                var text = IntegrationSettings.settings["SsoUrl"].ToString();
                if (!IntegrationSettings.UrlRegularExpression.IsMatch(text))
                {
                    throw new Exception(string.Format(Resources.InvalidUrlFormat, "SsoUrl"));
                }

                return text;
            }

            set
            {
                IntegrationSettings.settings["SsoUrl"] = value;
            }
        }
      
    }
}