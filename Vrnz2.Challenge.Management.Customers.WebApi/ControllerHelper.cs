using Microsoft.AspNetCore.Mvc;
using System;
using Vrnz2.Challenge.Management.Customers.Shared.Validations;
using Vrnz2.Challenge.Management.Customers.WebApi.CustomResults;
using Vrnz2.Challenge.ServiceContracts.ErrorMessageCodes;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models.Base;

namespace Vrnz2.Challenge.Management.Customers.WebApi
{
    public static class ControllerHelper
    {
        public static ObjectResult Return<TRequest, TResult>(ValidationHelper validationHelper, Func<TRequest, TResult> action, TRequest request)
            where TRequest : BaseRequestModel
        {
            var validation = validationHelper.Validate<TRequest>(request);

            if (validation.IsValid)
            {
                var response = action(request);

                return new OkObjectResult(response);
            }
            else if (validation.ErrorCodes.Contains(ErrorMessageCodesFactory.UNEXPECTED_ERROR))
            {
                return new InternalServerErrorObjectResult(validation.ErrorCodes);
            }
            else 
            {
                return new BadRequestObjectResult(validation.ErrorCodes);
            }
        }
    }
}
