﻿using Vrnz2.Challenge.ServiceContracts.Settings;

namespace Vrnz2.Challenge.Management.Customers.Shared.Settings
{
    public class AwsSettings
        : BaseAppSettings
    {
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
