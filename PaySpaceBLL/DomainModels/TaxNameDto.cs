namespace PaySpaceBLL.DomainModels
{
    public class TaxNameDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TaxRangeDto> TaxRanges { get; set; }
    }
}