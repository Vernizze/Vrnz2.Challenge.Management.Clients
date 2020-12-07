using Vrnz2.Challenge.ServiceContracts.Settings;

namespace Vrnz2.Challenge.Management.Customers.Shared.Settings
{
    public class QueuesSettings
        : BaseAppSettings
    {
        public string CustomerCreatedQueueName { get; set; }
    }
}
