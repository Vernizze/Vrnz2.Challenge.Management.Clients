using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;

namespace Vrnz2.Challenge.Management.Customers.UseCases.CreateCustomer
{
    public class CreateCustomer
        : IRequestHandler<CreateCustomerModel.Request, CreateCustomerModel.Response>
    {
        public Task<CreateCustomerModel.Response> Handle(CreateCustomerModel.Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
