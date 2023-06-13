using HelperLibrary;
using MediatR;
using PaySpaceBLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpaceBLL.Cqrs.Commands.CreateTaxRange;

public class TaxRangeCreateCommand : IRequest<ServiceResponse<TaxRangeCreateDto>>
{
    public TaxRangeCreateDto taxRange { get; set; }

    public TaxRangeCreateCommand(TaxRangeCreateDto taxRange)
    {
        this.taxRange = taxRange;
    }
}
