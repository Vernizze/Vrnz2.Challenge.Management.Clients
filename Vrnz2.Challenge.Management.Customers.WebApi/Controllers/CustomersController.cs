using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Vrnz2.Challenge.Management.Customers.Shared.Validations;
using Vrnz2.Challenge.ServiceContracts.UseCases.Models;

namespace Vrnz2.Challenge.Management.Customers.WebApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController 
        : ControllerBase
    {
        #region Variables

        private readonly ValidationHelper _validationHelper;
        private readonly IMediator _mediator;

        #endregion

        #region Constructors

        public CustomersController(ValidationHelper validationHelper, IMediator mediator)
        {
            _validationHelper = validationHelper; 
            _mediator = mediator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// [POST] Creation Customer end point
        /// </summary>
        /// <param name="request">Name, Cpf and State</param>
        /// <returns>Http Status Code 'OK'</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateCustomerModel.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(CreateCustomerModel.Request request) 
            => await ControllerHelper.ReturnAsync(_validationHelper, (request) => _mediator.Send(request), request);

        #endregion
    }
}
