using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;
using CreateCustomer = Vrnz2.Challenge.Management.Customers.UseCases.CreateCustomer;
using GetCustomer = Vrnz2.Challenge.Management.Customers.UseCases.GetCustomer;

namespace Vrnz2.Challenge.Management.Customers.Shared.Validations
{
    public static class ValiationServiceExtensions
    {
        public static IServiceCollection AddValidations(this IServiceCollection services)
            => services
                .AddScoped<IValidatorFactory, ValidatorFactory>()
                .AddScoped<ValidationHelper>()
                .AddTransient<IValidator<CreateCustomerModel.Request>, CreateCustomer.RequestValidator>()
                .AddTransient<IValidator<GetCustomerModel.Request>, GetCustomer.RequestValidator>();
    }
}
