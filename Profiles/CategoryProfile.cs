using AutoMapper;
using budget_planner_api.DTOs;
using budget_planner_api.Models;

namespace budget_planner_api.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryModelDTO>();

        CreateMap<CategoryModelDTO, Category>();
    }
}