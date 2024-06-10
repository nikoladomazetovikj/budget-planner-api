using AutoMapper;
using budget_planner_api.DTOs;
using budget_planner_api.Repositories.TypeRepository;

namespace budget_planner_api.Services.TypeService;

public class TypeService : ITypeService
{
    private readonly ITypeRepository _typeRepository;
    private readonly IMapper _mapper;

    public TypeService(ITypeRepository typeRepository, IMapper mapper)
    {
        _typeRepository = typeRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<TypeModelDTO>> ListTypesAsync()
    {
        var types = await _typeRepository.ListTypesAsync();

        return _mapper.Map<IEnumerable<TypeModelDTO>>(types);
    }

    public async Task<TypeModelDTO> GetTypeByIdAsync(int id)
    {
        var type = await _typeRepository.GetTypeById(id);

        return _mapper.Map<TypeModelDTO>(type);
    }
}