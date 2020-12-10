using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.Challenge.Management.Customers.Shared.Entities;
using Vrnz2.Challenge.Management.Customers.Shared.Queues;
using Vrnz2.Challenge.Management.Customers.Shared.Settings;
using Vrnz2.Challenge.ServiceContracts.Notifications;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;

namespace Vrnz2.Challenge.Management.Customers.UseCases.CreateCustomer
{
    public class CreateCustomer
        : IRequestHandler<CreateCustomerModel.Request, CreateCustomerModel.Response>
    {
        #region Variables

        private const string MONGODB_COLLECTION = "Client";
        private const string MONGODB_DATABASE = "Challenge";

        #endregion

        #region Variables

        private readonly ConnectionStringsSettings _connectionStringsSettings;
        private readonly QueuesSettings _queuesSettings;
        private readonly IMapper _mapper;
        private readonly QueueHandler _queueHandler;

        #endregion

        #region Constructor

        public CreateCustomer(IOptions<ConnectionStringsSettings> connectionStringsOptions, IOptions<QueuesSettings> queuesOptionsSettings, IMapper mapper, QueueHandler queueHandler)
        {
            _connectionStringsSettings = connectionStringsOptions.Value;
            _queuesSettings = queuesOptionsSettings.Value;
            _mapper = mapper;
            _queueHandler = queueHandler;
        }

        #endregion

        #region Methods

        public async Task<CreateCustomerModel.Response> Handle(CreateCustomerModel.Request request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);

            await SendToMongo(customer);
            
            await SendToQueue(_mapper.Map<CustomerNotification.Created>(request));

            return new CreateCustomerModel.Response
            {
                Success = true,
                Message = "Success",
                ClientUniqueId = customer.UniqueId
            };
        }

        public virtual async Task SendToMongo(Customer customer) 
        {
            using (var mongo = new Data.MongoDB.MongoDB(_connectionStringsSettings.MongoDbChallenge, MONGODB_COLLECTION, MONGODB_DATABASE))
                await mongo.Add(customer);
        }

        public virtual async Task SendToQueue(CustomerNotification.Created notification) 
            => await _queueHandler.Send(notification, _queuesSettings.CustomerCreatedQueueName);

        #endregion
    }
}
