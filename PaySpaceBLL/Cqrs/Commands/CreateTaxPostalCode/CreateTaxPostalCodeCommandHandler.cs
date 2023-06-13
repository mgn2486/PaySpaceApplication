using HelperLibrary;
using MediatR;
using PaySpaceBLL.Abstractions;
using PaySpaceBLL.DomainModels;

namespace PaySpaceBLL.Cqrs.Commands.CreateTaxPostalCode;

public class CreateTaxPostalCodeCommandHandler : IRequestHandler<CreateTaxPostalCodeCommand, ServiceResponse<PostalCodeDto>>
{
    private readonly IPaySpaceTaxService _paySpaceTaxService;

    public CreateTaxPostalCodeCommandHandler(IPaySpaceTaxService paySpaceTaxService)
    {
        _paySpaceTaxService = paySpaceTaxService;
    }

    public async Task<ServiceResponse<PostalCodeDto>> Handle(CreateTaxPostalCodeCommand request, CancellationToken cancellationToken)
    {
        return await _paySpaceTaxService.CreatePostalCodeAsync(request.PostalCode);
    }
}
