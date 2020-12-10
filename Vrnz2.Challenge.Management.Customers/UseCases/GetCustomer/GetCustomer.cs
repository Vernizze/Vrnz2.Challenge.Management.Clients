using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.Challenge.Management.Customers.Shared.Entities;
using Vrnz2.Challenge.Management.Customers.Shared.Settings;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;
using Vrnz2.Infra.CrossCutting.Extensions;
using Vrnz2.Infra.CrossCutting.Types;

namespace Vrnz2.Challenge.Management.Customers.UseCases.GetCustomer
{
    public class GetCustomer
        : IRequestHandler<GetCustomerModel.Request, GetCustomerModel.Response>
    {
        #region Variables

        private const string MONGODB_COLLECTION = "Client";
        private const string MONGODB_DATABASE = "Challenge";

        #endregion

        #region Variables

        private readonly ConnectionStringsSettings _connectionStringsSettings;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetCustomer(IOptions<ConnectionStringsSettings> connectionStringsOptions, IMapper mapper)
        {
            _connectionStringsSettings = connectionStringsOptions.Value;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<GetCustomerModel.Response> Handle(GetCustomerModel.Request request, CancellationToken cancellationToken)
        {
            var customer = await GetData(request);

            return _mapper.Map<GetCustomerModel.Response>(customer);
        }

        public virtual async Task<Customer> GetData(GetCustomerModel.Request request)
        {
            Customer result = null;

            using (var mongo = new Data.MongoDB.MongoDB(_connectionStringsSettings.MongoDbChallenge, MONGODB_COLLECTION, MONGODB_DATABASE))
            {
                var found = await mongo.GetMany<Customer>(c => c.Cpf.Equals(request.Cpf));

                if (found.HaveAny())
                    result = found.Single();
            }

            return result;
        }

        public bool IsNew(string cpf)
        {
            var customer = Handle(new GetCustomerModel.Request { Cpf = new Cpf(cpf).Value }, new CancellationToken()).Result;

            return customer.IsNull();
        }

        #endregion
    }
}
