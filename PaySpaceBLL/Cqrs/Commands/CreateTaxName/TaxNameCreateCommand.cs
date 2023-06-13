using HelperLibrary;
using PaySpaceBLL.DomainModels;
using MediatR;

namespace PaySpaceBLL.Cqrs.Commands.CreateTaxName
{
    public class TaxNameCreateCommand : IRequest<ServiceResponse<TaxNameCreateDto>>
    {
        public string Name { get; set; }

        public TaxNameCreateCommand(string name)
        {
            Name = name;
        }
    }
}
