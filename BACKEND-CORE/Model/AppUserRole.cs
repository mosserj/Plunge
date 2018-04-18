using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
  [Table("UserRole", Schema = "Security")]
  public class AppUserRole
  {
    [Required()]
    [Key()]
    public Guid RoleId { get; set; }

    [Required()]
    public Guid UserId { get; set; }

    [Required()]
    public string RoleType { get; set; }

    [Required()]
    public string RoleValue { get; set; }
  }
}
