using Vrnz2.Data.MongoDB.Entities.Base;

namespace Vrnz2.Challenge.Management.Customers.UseCases.CreateCustomer
{
    public class Customer
        : BaseMongoDbEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Estate { get; set; }
    }
}
