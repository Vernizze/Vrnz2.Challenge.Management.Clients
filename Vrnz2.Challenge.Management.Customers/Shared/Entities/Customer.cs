using System;
using Vrnz2.Data.MongoDB.Entities.Base;

namespace Vrnz2.Challenge.Management.Customers.Shared.Entities
{
    public class Customer
        : BaseMongoDbEntity
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string State { get; set; }
    }
}
