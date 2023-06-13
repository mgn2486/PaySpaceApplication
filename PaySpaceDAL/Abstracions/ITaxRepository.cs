using PaySpaceDAL.Entities;

namespace PaySpaceDAL.Abstracions;

public interface ITaxRepository
{
    Task<TaxName> CreateTaxNameAsync(TaxName newTaxName);
    
    Task<TaxRange> CreateTaxRangeAsync(TaxRange newTaxRangeModel);
    
    Task<List<PostalCode>> GetAllPostalCodes();
    
    Task<PostalCode> CreateTaxPostalCodeAsync(PostalCode newPostalCode);
    
    Task<TaxName> GetTaxNameByIdAsync(int taxNameId);
    
    Task<List<TaxRange>> GetTaxRangesByTaxNameIdAsync(int taxNameId);

    Task<List<TaxName>> GetAllTaxNamesAsync();

    Task<PostalCode> GetTaxPostalCodeDetailsByIdAsync(int postalCodeId);

    Task<PostalCode> GetTaxPostalCodeByNameAsync(string name);
}
