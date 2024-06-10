using AutoMapper;
using budget_planner_api.DTOs;

namespace budget_planner_api.Profiles;

public class TypeProfile : Profile
{
    public TypeProfile()
    {
        CreateMap<Models.Type, TypeModelDTO>();

        CreateMap<Models.Type, Type>();
    }
    
}