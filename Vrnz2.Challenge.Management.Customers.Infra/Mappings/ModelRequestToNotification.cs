using AutoMapper;
using System;
using Vrnz2.Challenge.ServiceContracts.Notifications;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;

namespace Vrnz2.Challenge.Management.Customers.Infra.Mappings
{
    public class ModelRequestToNotification
        : Profile
    {
        public ModelRequestToNotification()
        {
            CreateMap<CreateCustomerModel.Request, CustomerNotification.Created>()
                .ForMember(d => d.CreationDate, orig => orig.MapFrom(src => DateTime.UtcNow));
        }
    }
}
