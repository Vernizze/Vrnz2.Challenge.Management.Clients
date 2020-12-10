using AutoMapper;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Vrnz2.Challenge.Management.Customers.Shared.Entities;
using Vrnz2.Challenge.Management.Customers.Shared.Queues;
using Vrnz2.Challenge.Management.Customers.Shared.Settings;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;
using Vrnz2.Infra.CrossCutting.Types;
using Xunit;
using Get = Vrnz2.Challenge.Management.Customers.UseCases.GetCustomer;

namespace Vrnz2.Challenge.Management.Customers.Tests.UseCases.GetCustomer
{
    public class GetCustomerTest
    {
        private IMapper _mapper;
        private QueueHandler _queueHandler;
        private IOptions<ConnectionStringsSettings> _connectionStringsOptions;
        private IOptions<QueuesSettings> _queuesOptionsSettings;
        private IOptions<AwsSettings> _awsOptionsSettings;

        public GetCustomerTest()
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

        private GetCustomerMock GetInstance()
            => new GetCustomerMock(_connectionStringsOptions, _mapper);

        [Fact]
        public async Task CreatePayment_Handler_Test()
        {
            //Arrange
            var uniqueId = new Guid("ef01bedb-2d4c-418e-ac52-1e8a10b9b2a8");
            Cpf cpf = "434.443.474-99";
            var name = "Pedro de Oliveira";
            var state = "PR";

            var response = new GetCustomerModel.Response
            {
                ClientUniqueId = uniqueId,
                Cpf = cpf.Value,
                Name = name,
                State = state
            };

            _mapper.Map<GetCustomerModel.Response>(Arg.Any<Customer>()).Returns(response);

            var service = GetInstance();

            var request = new GetCustomerModel.Request
            {
                Cpf = cpf.Value
            };

            //Act
            var result = await service.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(uniqueId, result.ClientUniqueId);
            Assert.Equal(cpf.Value, result.Cpf);
            Assert.Equal(name, result.Name);
            Assert.Equal(state, result.State);
        }
    }

    public class GetCustomerMock
        : Get.GetCustomer
    {
        public GetCustomerMock(IOptions<ConnectionStringsSettings> connectionStringsOptions, IMapper mapper) 
            : base(connectionStringsOptions, mapper)
        {
        }

        public override Task<Customer> GetData(GetCustomerModel.Request request)
        {
            var uniqueId = new Guid("ef01bedb-2d4c-418e-ac52-1e8a10b9b2a8");
            Cpf cpf = "434.443.474-99";
            var name = "Pedro de Oliveira";
            var state = "PR";

            var result = new Customer
            {
                UniqueId = uniqueId,
                Cpf = cpf.Value,
                Name = name,
                State = state
            };

            return Task.FromResult(result);
        }
    }
}
