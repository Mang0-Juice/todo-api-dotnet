using AutoMapper;

namespace ToDoList.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskItem, TaskReadDTO>().ReverseMap();
        CreateMap<TaskCreateDTO, TaskItem>().ReverseMap();
    }
    
}