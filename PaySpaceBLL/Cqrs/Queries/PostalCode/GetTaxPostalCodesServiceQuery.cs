using HelperLibrary;
using MediatR;
using PaySpaceBLL.DomainModels;

namespace PaySpaceBLL.Cqrs.Queries.PostalCode;

public class GetTaxPostalCodesServiceQuery : IRequest<ServiceResponse<List<GetPostalCodeDto>>>
{
}
