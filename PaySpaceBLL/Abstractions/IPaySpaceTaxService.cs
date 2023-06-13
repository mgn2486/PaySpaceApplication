using HelperLibrary;
using PaySpaceBLL.DomainModels;

namespace PaySpaceBLL.Abstractions;

public interface IPaySpaceTaxService
{
    Task<ServiceResponse<List<GetPostalCodeDto>>> GetAllPostalCodes();

    Task<ServiceResponse<TaxNameCreateDto>> CreateTaxNameAsync(string taxName);

    Task<ServiceResponse<TaxRangeCreateDto>> CreateTaxRangeAsync(TaxRangeCreateDto taxRange);

    Task<ServiceResponse<PostalCodeDto>> CreatePostalCodeAsync(PostalCodeDto postalCode);

    Task<ServiceResponse<List<TaxNameDto>>> GetTaxNamesAsync();

    Task<ServiceResponse<GetCalculatedTaxDto>> CalculateAnnualTaxAsync(CalculateTaxRequestDto calculateTaxRequest);
}
