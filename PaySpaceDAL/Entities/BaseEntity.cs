using System.ComponentModel.DataAnnotations;

namespace PaySpaceDAL.Entities;

public class BaseEntity
{
    [Required]
    public string CreatedBy { get; set; } = "SysAdmin";

    [Required]
    public DateTime DateCreated { get; set; } = DateTime.Now;

    public string UpdatedBy { get; set; } = "SysAdmin";

    public DateTime DateUpdated { get; set; } = DateTime.Now;
}