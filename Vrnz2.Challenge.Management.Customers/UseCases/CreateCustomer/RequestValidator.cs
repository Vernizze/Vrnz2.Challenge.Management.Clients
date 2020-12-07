using FluentValidation;
using Vrnz2.Challenge.ServiceContracts.ErrorMessageCodes;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;
using Vrnz2.Infra.Crosscutting.Types;
using Get = Vrnz2.Challenge.Management.Customers.UseCases.GetCustomer;

namespace Vrnz2.Challenge.Management.Customers.UseCases.CreateCustomer
{
    public class RequestValidator
        : AbstractValidator<CreateCustomerModel.Request>
    {
        public RequestValidator(Get.GetCustomer getCustomer)
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage(ErrorMessageCodesFactory.INVALID_CUSTOMER_NAME_ERROR);

            RuleFor(v => v.Cpf)
                .Must(Cpf.IsValid)
                .WithMessage(ErrorMessageCodesFactory.INVALID_ITR_ERROR);

            RuleFor(v => v.Cpf)
                .Must(getCustomer.IsNew)
                .WithMessage(ErrorMessageCodesFactory.INVALID_ITR_ERROR);

            RuleFor(v => v.State)
                .NotEmpty()
                .WithMessage(ErrorMessageCodesFactory.INVALID_CUSTOMER_RESIDENCE_STATE_ERROR);
        }
    }
}
