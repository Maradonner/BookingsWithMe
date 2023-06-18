using AutoMapper;
using BookingsWithMe.Entities;
using BookingsWithMe.Models.User;

namespace BookingsWithMe.Mapper;

public class UserProfiler : Profile
{
    public UserProfiler()
    {
        CreateMap<User, UserForCreationDto>().ReverseMap();


        CreateMap<User, UserForDisplayDto>()
            .ForMember(dest => dest.Availabilities, opt => opt.MapFrom(src => src.Availabilities))
            .ForMember(dest => dest.BlockedTimes, opt => opt.MapFrom(src => src.BlockedTimes))
            .ForMember(dest => dest.Bookings, opt => opt.MapFrom(src => src.Bookings));

        CreateMap<UserForDisplayDto, User>()
            .ForMember(dest => dest.Availabilities, opt => opt.MapFrom(src => src.Availabilities))
            .ForMember(dest => dest.BlockedTimes, opt => opt.MapFrom(src => src.BlockedTimes))
            .ForMember(dest => dest.Bookings, opt => opt.MapFrom(src => src.Bookings));


        CreateMap<UserForDisplayDto, User>();

        CreateMap<User, UserForManipulationDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
    }
}