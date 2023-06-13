using HelperLibrary;
using MediatR;
using PaySpaceBLL.Abstractions;
using PaySpaceBLL.DomainModels;

namespace PaySpaceBLL.Cqrs.Commands.CreateTaxName;

public class TaxNameCreateCommandHandler : IRequestHandler<TaxNameCreateCommand, ServiceResponse<TaxNameCreateDto>>
{
    private readonly IPaySpaceTaxService _paySpaceTaxService;

    public TaxNameCreateCommandHandler(IPaySpaceTaxService paySpaceTaxService)
    {
        _paySpaceTaxService = paySpaceTaxService;
    }

    public async Task<ServiceResponse<TaxNameCreateDto>> Handle(TaxNameCreateCommand request, CancellationToken cancellationToken)
    {
        var response = await _paySpaceTaxService.CreateTaxNameAsync(request.Name);
        return response;
    }
}
