using HelperLibrary;
using MediatR;
using PaySpaceBLL.DomainModels;

namespace PaySpaceBLL.Cqrs.Queries.TaxCalculater;

public class CalculateTaxQuery : IRequest<ServiceResponse<GetCalculatedTaxDto>>
{
    public CalculateTaxRequestDto CalculateTaxRequest { get; set; }

    public CalculateTaxQuery(CalculateTaxRequestDto calculateTaxRequest)
    {
        this.CalculateTaxRequest = calculateTaxRequest;
    }
}