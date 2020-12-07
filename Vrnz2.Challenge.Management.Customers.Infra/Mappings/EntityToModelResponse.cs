using AutoMapper;
using Vrnz2.Challenge.Management.Customers.Shared.Entities;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;

namespace Vrnz2.Challenge.Management.Customers.Infra.Mappings
{
    public class EntityToModelResponse
        : Profile
    {
        public EntityToModelResponse()
        {
            CreateMap<Customer, GetCustomerModel.Response>();
        }
    }
}
