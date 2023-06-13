using PaySpaceDAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace PaySpaceDAL
{
    public class TaxRange : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public double Rate { get; set; }

        public int StartAmount { get; set; }

        public int EndAmount { get; set; }

        public int? TaxNameId { get; set; }

        public TaxName TaxName { get; set; }
    }
}