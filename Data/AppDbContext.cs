using budget_planner_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Type = budget_planner_api.Models.Type;

namespace budget_planner_api.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food" },
                new Category { Id = 2, Name = "Utilities" },
                new Category { Id = 3, Name = "Transportation" },
                new Category { Id = 4, Name = "Entertainment" },
                new Category { Id = 5, Name = "Health" },
                new Category { Id = 6, Name = "Housing" },
                new Category { Id = 7, Name = "Insurance" },
                new Category { Id = 8, Name = "Savings" },
                new Category { Id = 9, Name = "Investments" },
                new Category { Id = 10, Name = "Debt Payments" },
                new Category { Id = 11, Name = "Clothing" },
                new Category { Id = 12, Name = "Education" },
                new Category { Id = 13, Name = "Gifts" },
                new Category { Id = 14, Name = "Charity" },
                new Category { Id = 15, Name = "Personal Care" },
                new Category { Id = 16, Name = "Pets" },
                new Category { Id = 17, Name = "Subscriptions" },
                new Category { Id = 18, Name = "Travel" },
                new Category { Id = 19, Name = "Miscellaneous" },
                new Category { Id = 20, Name = "Bills" }
            );

            
            modelBuilder.Entity<Type>().HasData(
                new Type { Id = 1, Name = "Income" },
                new Type { Id = 2, Name = "Expense" }
            );
        }
    }
}
