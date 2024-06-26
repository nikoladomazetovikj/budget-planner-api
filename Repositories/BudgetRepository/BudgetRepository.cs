﻿using budget_planner_api.Data;
using budget_planner_api.Models;
using Microsoft.EntityFrameworkCore;

namespace budget_planner_api.Repositories.BudgetRepository;

public class BudgetRepository : IBudgetRepository
{
    private readonly AppDbContext _context;

    public BudgetRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Budget>> ListBudgetsAsync()
    {
        return await _context.Budgets
                            .Include(b => b.Category)
                            .Include(b => b.Type)
                            .ToListAsync();
    }

    public async Task<Budget> AddBudgetAsync(Budget budget)
    {
        if (budget == null)
            throw new ArgumentNullException(nameof(budget));

        await _context.Budgets.AddAsync(budget);
        await _context.SaveChangesAsync();

        return budget;
    }

    public async Task<bool> DeleteBudgetAsync(int id)
    {
        var existingBudget = await _context.Budgets.FindAsync(id);
    
        if (existingBudget == null)
        {
            return false;
        }

        _context.Budgets.Remove(existingBudget);
        var result = await _context.SaveChangesAsync();
        return result > 0; 
    }

    public async Task<Budget> GetBudgetByIdAsync(int id)
    {
        return await _context.Budgets.FindAsync(id);
    }

    public async Task<IEnumerable<Budget>> ListBudgetsByUserIdAsync(string userId)
    {
        return await _context.Budgets.Where(b => b.UserId == userId).ToListAsync();
    }
}