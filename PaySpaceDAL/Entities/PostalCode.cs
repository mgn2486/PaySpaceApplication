using PaySpaceDAL.Entities;

namespace PaySpaceDAL
{
    public class PostalCode : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? TaxNameId { get; set; }
        public TaxName TaxName { get; set; }
    }
}