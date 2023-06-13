using HelperLibrary;
using MediatR;
using PaySpaceBLL.DomainModels;

namespace PaySpaceBLL.Cqrs.Queries.TaxName
{
    public class GetTaxNamesQuery : IRequest<ServiceResponse<List<TaxNameDto>>>
    {
    }
}
