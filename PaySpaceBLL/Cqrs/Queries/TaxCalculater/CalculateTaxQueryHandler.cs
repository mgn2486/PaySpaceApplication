using HelperLibrary;
using MediatR;
using PaySpaceBLL.Abstractions;
using PaySpaceBLL.DomainModels;

namespace PaySpaceBLL.Cqrs.Queries.TaxCalculater;

public class CalculateTaxQueryHandler : IRequestHandler<CalculateTaxQuery, ServiceResponse<GetCalculatedTaxDto>>
{
    private readonly IPaySpaceTaxService _paySpaceTaxService;

    public CalculateTaxQueryHandler(IPaySpaceTaxService paySpaceTaxService)
    {
        _paySpaceTaxService = paySpaceTaxService;
    }

    public async Task<ServiceResponse<GetCalculatedTaxDto>> Handle(CalculateTaxQuery request, CancellationToken cancellationToken)
    {
        return await _paySpaceTaxService.CalculateAnnualTaxAsync(request.CalculateTaxRequest);
    }
}
