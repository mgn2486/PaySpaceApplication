using Azure.Core;
using HelperLibrary;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaySpaceApplication.Models;
using PaySpaceBLL.Cqrs.Commands.CreateTaxName;
using PaySpaceBLL.Cqrs.Commands.CreateTaxPostalCode;
using PaySpaceBLL.Cqrs.Commands.CreateTaxRange;
using PaySpaceBLL.Cqrs.Queries.PostalCode;
using PaySpaceBLL.Cqrs.Queries.TaxCalculater;
using PaySpaceBLL.DomainModels;

namespace PaySpaceApplication.Controllers
{
    public class TaxController : Controller
    {
        private readonly IMediator _mediator;

        public TaxController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the application tax codes based on description and id value
        /// </summary>
        /// <param name="taxCalculateRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResponse<List<GetPostalCodeDto>>> GetTaxPostalCodes()
        {
            var request = await _mediator.Send(new GetTaxPostalCodesServiceQuery());
            return request;
        }

        /// <summary>
        /// Creates a new Tax Name 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResponse<TaxNameCreateDto>> CreateTaxName([FromBody] TaxNameCreateDto request)
        {
            return await _mediator.Send(new TaxNameCreateCommand(request.Name));
        }


        /// <summary>
        /// This creates a new Tax Range
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResponse<TaxRangeCreateDto>> CreateTaxRangeRequest([FromBody] TaxRangeCreateDto request)
        {
            return await _mediator.Send(new TaxRangeCreateCommand(request));
        }


        /// <summary>
        /// This creates a new Tax Postal Code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResponse<PostalCodeDto>> CreateTaxPostal([FromBody] PostalCodeDto request)
        {
            return await _mediator.Send(new CreateTaxPostalCodeCommand(request));
        }


        /// <summary>
        /// Calculates the tax based on the Annaula Salary and the Postal Code Provided
        /// </summary>
        /// <param name="taxCalculateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResponse<GetCalculatedTaxDto>> CalculateSalaryTax([FromBody] CalculateTaxRequestDto taxCalculateRequest)
        {
            return await _mediator.Send(new CalculateTaxQuery(taxCalculateRequest));
        }
    }
}
 