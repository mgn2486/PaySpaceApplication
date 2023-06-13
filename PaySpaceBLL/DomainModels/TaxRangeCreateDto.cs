namespace PaySpaceBLL.DomainModels;

public class TaxRangeCreateDto
{
    public double Rate { get; set; }

    public int? StartAmount { get; set; }

    public int? EndAmount { get; set; }

    public int TaxNameId { get; set; }
}
