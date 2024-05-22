using System.ComponentModel.DataAnnotations;

namespace budget_planner_api.Models;

public class Type
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; }
}