using HelperLibrary;
using MediatR;
using PaySpaceBLL.Abstractions;
using PaySpaceBLL.DomainModels;

namespace PaySpaceBLL.Cqrs.Queries.PostalCode;

public class GetTaxPostalCodesServiceQueryHandler : IRequestHandler<GetTaxPostalCodesServiceQuery, ServiceResponse<List<GetPostalCodeDto>>>
{
    private readonly IPaySpaceTaxService _paySpaceTaxService;

    public GetTaxPostalCodesServiceQueryHandler(IPaySpaceTaxService paySpaceTaxService) 
        => _paySpaceTaxService = paySpaceTaxService;

    public async Task<ServiceResponse<List<GetPostalCodeDto>>> Handle(GetTaxPostalCodesServiceQuery request, CancellationToken cancellationToken) 
        => await _paySpaceTaxService.GetAllPostalCodes();
}
