using HelperLibrary;
using MediatR;
using PaySpaceBLL.Abstractions;
using PaySpaceBLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpaceBLL.Cqrs.Commands.CreateTaxRange;

public class TaxRangeCreateCommandHandler : IRequestHandler<TaxRangeCreateCommand, ServiceResponse<TaxRangeCreateDto>>
{
    private readonly IPaySpaceTaxService _paySpaceTaxService;

    public TaxRangeCreateCommandHandler(IPaySpaceTaxService paySpaceTaxService) 
        => _paySpaceTaxService = paySpaceTaxService;

    public async Task<ServiceResponse<TaxRangeCreateDto>> Handle(TaxRangeCreateCommand request, CancellationToken cancellationToken = default) 
        => await _paySpaceTaxService.CreateTaxRangeAsync(request.taxRange);
}
