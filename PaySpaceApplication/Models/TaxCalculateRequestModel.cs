using System.ComponentModel;

namespace PaySpaceApplication.Models
{
    public class TaxCalculateRequestModel
    {
        [DisplayName("Annual Salary")]
        public string AnnualSalary { get; set; }

        [DisplayName("Postal Code")]
        public string PostalCodeId { get; set; }
    }
}
