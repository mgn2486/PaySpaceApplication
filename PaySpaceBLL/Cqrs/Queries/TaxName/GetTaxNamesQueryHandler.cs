using HelperLibrary;
using MediatR;
using PaySpaceBLL.Abstractions;
using PaySpaceBLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpaceBLL.Cqrs.Queries.TaxName;

public class GetTaxNamesQueryHandler : IRequestHandler<GetTaxNamesQuery, ServiceResponse<List<TaxNameDto>>>
{
    private readonly IPaySpaceTaxService _paySpaceTaxService;

    public GetTaxNamesQueryHandler(IPaySpaceTaxService paySpaceTaxService)
    {
        _paySpaceTaxService = paySpaceTaxService;
    }

    public async Task<ServiceResponse<List<TaxNameDto>>> Handle(GetTaxNamesQuery request, CancellationToken cancellationToken)
    {
        return await _paySpaceTaxService.GetTaxNamesAsync();
    }
}
