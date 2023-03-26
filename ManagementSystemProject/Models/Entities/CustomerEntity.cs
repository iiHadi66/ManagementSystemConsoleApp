using System.ComponentModel.DataAnnotations;

namespace ManagementSystemProject.Models.Entities;

public class CustomerEntity
{
    [Key]
    public int ID { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}
