using HelperLibrary;
using MediatR;
using PaySpaceBLL.DomainModels;

namespace PaySpaceBLL.Cqrs.Commands.CreateTaxPostalCode;

public class CreateTaxPostalCodeCommand : IRequest<ServiceResponse<PostalCodeDto>>
{
    public PostalCodeDto PostalCode { get; set; }

    public CreateTaxPostalCodeCommand(PostalCodeDto postalCode)
    {
        PostalCode = postalCode;
    }
}
