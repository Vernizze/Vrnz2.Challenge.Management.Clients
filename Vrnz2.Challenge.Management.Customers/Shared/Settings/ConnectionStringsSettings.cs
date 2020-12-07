using Vrnz2.Challenge.ServiceContracts.Settings;

namespace Vrnz2.Challenge.Management.Customers.Shared.Settings
{
    public class ConnectionStringsSettings
        : BaseAppSettings
    {
        public string MongoDbChallenge { get; set; }
    }
}
