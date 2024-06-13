using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace budget_planner_api.Models;

public class Budget
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; }
    
    [MaxLength(10000)]
    public string Description { get; set; }
    
    [Required]
    public DateTime OnDate { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    [Required]
    public int TypeId { get; set; }
    public Type Type { get; set; }
    
    [Required]
    public string UserId { get; set; }
}