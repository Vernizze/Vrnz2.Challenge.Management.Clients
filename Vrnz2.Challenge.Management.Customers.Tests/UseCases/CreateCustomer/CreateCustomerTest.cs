using AutoMapper;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Vrnz2.Challenge.Management.Customers.Shared.Entities;
using Vrnz2.Challenge.Management.Customers.Shared.Queues;
using Vrnz2.Challenge.Management.Customers.Shared.Settings;
using Vrnz2.Challenge.ServiceContracts.Notifications;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;
using Vrnz2.Infra.CrossCutting.Types;
using Xunit;
using Create = Vrnz2.Challenge.Management.Customers.UseCases.CreateCustomer;

namespace Vrnz2.Challenge.Management.Customers.Tests.UseCases.CreateCustomer
{
    public class CreateCustomerTest
    {
        private IMapper _mapper;
        private QueueHandler _queueHandler;
        private IOptions<ConnectionStringsSettings> _connectionStringsOptions;
        private IOptions<QueuesSettings> _queuesOptionsSettings;
        private IOptions<AwsSettings> _awsOptionsSettings;

        public CreateCustomerTest()
        {
            _connectionStringsOptions = Options.Create(new ConnectionStringsSettings
            {
                MongoDbChallenge = string.Empty
            });

            _queuesOptionsSettings = Options.Create(new QueuesSettings
            {
                CustomerCreatedQueueName = "fila-teste"
            });

            _awsOptionsSettings = Options.Create(new AwsSettings
            {
                AccessKey = "XXX",
                Region = "XXX",
                SecretKey = "XXX"
            });

            _mapper = Substitute.For<IMapper>();
            _queueHandler = new QueueHandler(_awsOptionsSettings);
        }

        private CreateCustomerMock GetInstance()
            => new CreateCustomerMock(_connectionStringsOptions, _queuesOptionsSettings, _mapper, _queueHandler);

        [Fact]
        public async Task CreateCustomer_Handler_Test()
        {
            //Arrange      
            var uniqueId = new Guid("ef01bedb-2d4c-418e-ac52-1e8a10b9b2a8");
            Cpf cpf = "434.443.474-99";
            var name = "Pedro de Oliveira";
            var state = "PR";

            var customer = new Customer
            {
                UniqueId = uniqueId,
                Cpf = cpf.Value,
                Name = name,
                State = state
            };

            _mapper.Map<Customer>(Arg.Any<CreateCustomerModel.Request>()).Returns(customer);

            var service = GetInstance();

            var request = new CreateCustomerModel.Request
            {
                Cpf = cpf.Value,
                Name = name,
                State = state
            };

            //Act
            var result = await service.Handle(request, new System.Threading.CancellationToken());

            //Assert 
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Success", result.Message);
            Assert.Equal(uniqueId, result.ClientUniqueId);
        }
    }

    public class CreateCustomerMock
        : Create.CreateCustomer
    {
        public CreateCustomerMock(
            IOptions<ConnectionStringsSettings> connectionStringsOptions, 
            IOptions<QueuesSettings> queuesOptionsSettings, 
            IMapper mapper, 
            QueueHandler queueHandler) 
            : base(connectionStringsOptions, queuesOptionsSettings, mapper, queueHandler)
        {
        }

        public override Task SendToMongo(Customer customer)
            => Task.CompletedTask;

        public override Task SendToQueue(CustomerNotification.Created notification)
            => Task.CompletedTask;
    }
}
