using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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

        private readonly ILogger _logger;
        private readonly ControllerHelper _controllerHelper;
        private readonly ValidationHelper _validationHelper;        
        private readonly IMediator _mediator;

        #endregion

        #region Constructors

        public CustomersController(ILogger logger, ControllerHelper controllerHelper, ValidationHelper validationHelper, IMediator mediator)
        {
            _logger = logger;
            _controllerHelper = controllerHelper;
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
            => await _controllerHelper.ReturnAsync((request) => _mediator.Send(request), request);

        /// <summary>
        /// [GET] Get Customer data end point
        /// </summary>
        /// <param name="request">Cpf</param>
        /// <returns>Customer data</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetCustomerModel.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] string cpf)
        {
            var request = new GetCustomerModel.Request { Cpf = cpf };

            return await _controllerHelper.ReturnAsync((request) => _mediator.Send(request), request);
        }

        #endregion
    }
}
