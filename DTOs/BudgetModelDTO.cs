﻿namespace budget_planner_api.DTOs;

public class BudgetModelDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OnDate { get; set; }
    public int CategoryId { get; set; }
    public int TypeId { get; set; }
    public decimal Price { get; set; }
    public string UserId { get; set; }
}