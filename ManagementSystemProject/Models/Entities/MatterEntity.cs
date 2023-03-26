using System.ComponentModel.DataAnnotations;

namespace ManagementSystemProject.Models.Entities;

public class MatterEntity
{
    [Key]
    public int MatterID { get; set; }
    public int CustomerID { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Time { get; set; }
    public string Status { get; set; } = null!;
}
