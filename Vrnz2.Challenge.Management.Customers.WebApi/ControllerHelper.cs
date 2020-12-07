using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vrnz2.Challenge.Management.Customers.Shared.Validations;
using Vrnz2.Challenge.Management.Customers.WebApi.CustomResults;
using Vrnz2.Challenge.ServiceContracts.ErrorMessageCodes;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models.Base;

namespace Vrnz2.Challenge.Management.Customers.WebApi
{
    public static class ControllerHelper
    {
        public static async Task<ObjectResult> ReturnAsync<TRequest, TResult>(ValidationHelper validationHelper, Func<TRequest, Task<TResult>> action, TRequest request)
            where TRequest : BaseRequestModel
        {
            var validation = validationHelper.Validate<TRequest>(request);

            if (validation.IsValid)
            {
                var response = await action(request);

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
