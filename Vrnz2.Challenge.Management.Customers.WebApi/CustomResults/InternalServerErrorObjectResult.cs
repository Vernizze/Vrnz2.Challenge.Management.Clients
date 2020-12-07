using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vrnz2.Challenge.Management.Customers.WebApi.CustomResults
{
    public class InternalServerErrorObjectResult
        : ObjectResult
    {
        public InternalServerErrorObjectResult([ActionResultObjectValue] ModelStateDictionary modelState)
            : base(modelState)
        {        
        }

        public InternalServerErrorObjectResult([ActionResultObjectValue] object error)
            : base(error)
        {
        }
    }
}
