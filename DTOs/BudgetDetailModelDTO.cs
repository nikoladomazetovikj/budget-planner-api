namespace budget_planner_api.DTOs;

public class BudgetDetailModelDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OnDate { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    
    public decimal Price { get; set; }
    public int TypeId { get; set; }
    public string TypeName { get; set; }
    public string UserId { get; set; }
}