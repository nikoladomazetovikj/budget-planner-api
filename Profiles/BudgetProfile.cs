using AutoMapper;
using budget_planner_api.DTOs;
using budget_planner_api.Models;

namespace budget_planner_api.Profiles;

public class BudgetProfile : Profile
{
    public BudgetProfile()
    {
        CreateMap<Budget, BudgetModelDTO>();
        CreateMap<BudgetModelDTO, Budget>();
    }
}