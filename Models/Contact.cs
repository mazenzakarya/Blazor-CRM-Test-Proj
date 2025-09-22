using System.ComponentModel.DataAnnotations;

namespace Blazor_CRM_Test_Proj.Models;

public class Contact
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Subject { get; set; } = string.Empty;
    
    [Required]
    [StringLength(2000)]
    public string Message { get; set; } = string.Empty;
    
    public DateTime ContactDate { get; set; } = DateTime.UtcNow;
    
    [Required]
    [StringLength(50)]
    public string ContactType { get; set; } = "General"; // Email, Phone, Meeting, etc.
    
    public bool IsFollowUpRequired { get; set; } = false;
    
    public DateTime? FollowUpDate { get; set; }
    
    // Foreign Key
    public int CustomerId { get; set; }
    
    // Navigation property
    public virtual Customer? Customer { get; set; }
}

