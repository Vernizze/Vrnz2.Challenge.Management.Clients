using FluentValidation;
using Vrnz2.Challenge.ServiceContracts.ErrorMessageCodes;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;
using Vrnz2.Infra.CrossCutting.Types;

namespace Vrnz2.Challenge.Management.Customers.UseCases.GetCustomer
{
    public class RequestValidator
        : AbstractValidator<GetCustomerModel.Request>
    {
        public RequestValidator()
        {
            RuleFor(v => v.Cpf)
                .Must(Cpf.IsValid)
                .WithMessage(ErrorMessageCodesFactory.INVALID_ITR_ERROR);
        }
    }
}
