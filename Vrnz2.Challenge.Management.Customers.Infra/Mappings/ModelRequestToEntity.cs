using AutoMapper;
using System;
using Vrnz2.Challenge.Management.Customers.Shared.Entities;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;

namespace Vrnz2.Challenge.Management.Customers.Infra.Mappings
{
    public class ModelRequestToEntity
        : Profile
    {
        public ModelRequestToEntity() 
        {
            CreateMap<CreateCustomerModel.Request, Customer>()
                .ForMember(d => d.UniqueId, orig => orig.MapFrom(src => Guid.NewGuid()));
        }
    }
}
