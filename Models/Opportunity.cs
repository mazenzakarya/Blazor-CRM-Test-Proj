using System.ComponentModel.DataAnnotations;

namespace Blazor_CRM_Test_Proj.Models;

public class Opportunity
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "New"; // New, Qualified, Proposal, Negotiation, Closed Won, Closed Lost
    
    public decimal EstimatedValue { get; set; }
    
    public decimal? ActualValue { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? ExpectedCloseDate { get; set; }
    
    public DateTime? ActualCloseDate { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    // Foreign Key
    public int CustomerId { get; set; }
    
    // Navigation property
    public virtual Customer? Customer { get; set; }
}

