using AutoMapper;
using BookingsWithMe.Entities;
using BookingsWithMe.Models.User;

namespace BookingsWithMe.Mapper;

public class UserProfiler : Profile
{
    public UserProfiler()
    {
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForDisplayDto>().ReverseMap();
        CreateMap<User, UserForManipulationDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
    }
}
