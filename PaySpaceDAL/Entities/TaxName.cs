using Azure;
using System.ComponentModel.DataAnnotations;

namespace PaySpaceDAL.Entities;

public class TaxName : BaseEntity
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<PostalCode> PostalCodes { get; set; } = new List<PostalCode>();
    public virtual ICollection<TaxRange> TaxRanges { get; set; } = new List<TaxRange>();

}