using Vrnz2.Challenge.ServiceContracts.Settings;

namespace Vrnz2.Challenge.Management.Customers.Shared.Settings
{
    public class AppSettings
        : BaseAppSettings
    {
        public ConnectionStringsSettings ConnectionStrings { get; set; }
        public AwsSettings AwsSettings { get; set; }
    }
}
