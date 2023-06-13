using Microsoft.EntityFrameworkCore;
using PaySpaceDAL.Abstracions;
using PaySpaceDAL.Entities;

namespace PaySpaceDAL.Repositories;

public class TaxRepository : ITaxRepository
{
    private readonly PaySpaceDbContext _paySpaceDbContext;

    public TaxRepository(PaySpaceDbContext paySpaceDbContext)
    {
        _paySpaceDbContext = paySpaceDbContext;
    }

    public async Task<TaxName> CreateTaxNameAsync(TaxName newTaxName)
    {
        var entityModel = _paySpaceDbContext.TaxNames.Add(newTaxName);

        await _paySpaceDbContext.SaveChangesAsync();

        var createdEntityModel = entityModel.Entity;

        return createdEntityModel;
    }

    public async Task<PostalCode> CreateTaxPostalCodeAsync(PostalCode newPostalCode)
    {
        var entityModel = _paySpaceDbContext.PostalCodes.Add(newPostalCode);

        await _paySpaceDbContext.SaveChangesAsync();

        var createdEntityModel = entityModel.Entity;

        return createdEntityModel;
    }

    public async Task<TaxRange> CreateTaxRangeAsync(TaxRange newTaxRangeModel)
    {
        var entityModel = _paySpaceDbContext.TaxRanges.Add(newTaxRangeModel);

        await _paySpaceDbContext.SaveChangesAsync();

        var createdEntityModel = entityModel.Entity;

        return createdEntityModel;
    }

    public async Task<List<PostalCode>> GetAllPostalCodes()
    {
        return await _paySpaceDbContext.PostalCodes.ToListAsync();
    }

    public async Task<List<TaxName>> GetAllTaxNamesAsync()
    {
        var result = await _paySpaceDbContext.TaxNames
                                .Include( r => r.TaxRanges)
                                .ToListAsync();

        if (result is not null)
            return result;

        return null;
    }

    public async Task<TaxName> GetTaxNameByIdAsync(int taxNameId)
    {
        var result = await _paySpaceDbContext.TaxNames.FirstOrDefaultAsync(x => x.Id == taxNameId);

        if (result is not null)
            return result;

        return null;
    }

    public async Task<PostalCode?> GetTaxPostalCodeByNameAsync(string name)
    {
        var postalCode = await _paySpaceDbContext.PostalCodes.FirstOrDefaultAsync(y => y.Name == name);
        return postalCode is not null ? postalCode : null;
    }

    public async Task<PostalCode> GetTaxPostalCodeDetailsByIdAsync(int postalCodeId)
    {
        var result = await _paySpaceDbContext.PostalCodes
                        .Include(x => x.TaxName)
                        .Include(y => y.TaxName.TaxRanges)
                        .FirstOrDefaultAsync(x => x.Id == postalCodeId);

        return result is not null ? result : null;
    }

    public async Task<List<TaxRange>> GetTaxRangesByTaxNameIdAsync(int taxNameId)
    {
        return await _paySpaceDbContext.TaxRanges
                        .Where(x => x.TaxNameId == taxNameId)
                        .ToListAsync();
    }
}
