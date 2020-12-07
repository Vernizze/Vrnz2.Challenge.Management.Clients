using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Vrnz2.Challenge.Management.Customers.Shared.Settings;

namespace Vrnz2.Challenge.Management.Customers.UseCases.CreateCustomer
{
    public static class CreateCustomerServiceExtensions
    {
        public static IServiceCollection AddConsumers(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddMassTransit(x =>
            {
                x.UsingAmazonSqs((context, cfg) =>
                {
                    cfg.Host(appSettings.AwsSettings.Region, h =>
                    {
                        h.AccessKey(appSettings.AwsSettings.AccessKey);
                        h.SecretKey(appSettings.AwsSettings.SecretKey);
                    });

                    var successQueue = Environment.GetEnvironmentVariable("SuccessQueue");
                    var prefetchCount = ushort.Parse(Environment.GetEnvironmentVariable("PrefetchCount"));
                    var concurrencyLimit = ushort.Parse(Environment.GetEnvironmentVariable("ConcurrencyLimit"));
                    var retryNumber = ushort.Parse(Environment.GetEnvironmentVariable("RetryNumber"));
                    var timeInSecondsToRetry = ushort.Parse(Environment.GetEnvironmentVariable("TimeInSecondsToRetry"));

                    cfg.ReceiveEndpoint(successQueue, e =>
                    {
                        e.PrefetchCount = prefetchCount;
                        e.UseConcurrencyLimit(concurrencyLimit);
                        e.UseMessageRetry(x => x.Interval(retryNumber, TimeSpan.FromSeconds(timeInSecondsToRetry)));
                        e.Consumer(() => new ConsumerMock());
                    });
                });
            });

            return services;
        }
    }

    public class MessageMock
    {
        public string Nome { get; set; }
    }

    public class ConsumerMock
        : IConsumer<MessageMock>
    {
        public Task Consume(ConsumeContext<MessageMock> context)
        {
            throw new NotImplementedException();
        }
    }
}
