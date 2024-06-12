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
        
        CreateMap<Budget, BudgetDetailModelDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Name));
    }
}