namespace Nexcel.LRF.Integration.Configurations
{
    /// <summary>
    ///
    /// </summary>
    public interface IIntegrationSettings
    {
        string PublicKeyPath
        {
            get;
            set;
        }

        string SsoUrl
        {
            get;
            set;
        }

        string InternalLoginUrl
        {
            get;
            set;
        }

        string LicenseeName
        {
            get;
            set;
        }
    }
}