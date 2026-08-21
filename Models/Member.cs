using System.ComponentModel.DataAnnotations;

namespace GymMembershipManagement.Models;
public class Member
{
    public int Id { get; set; }
    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;
    [StringLength(600)]
    public string? Description { get; set; }
    [Required, StringLength(30)]
    public string Status { get; set; } = "Active";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
