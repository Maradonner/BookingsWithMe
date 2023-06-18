using AutoMapper;
using BookingsWithMe.Entities;
using BookingsWithMe.Models.Availabilitie;

namespace BookingsWithMe.Mapper;

public class AvailabilityProfiler : Profile
{
    public AvailabilityProfiler()
    {
        CreateMap<Availability, AvailabilityForCreationDto>().ReverseMap();

        CreateMap<Availability, AvailabilityForDisplayDto>().ReverseMap();

        CreateMap<Availability, AvailabilityForManipulationDto>()
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Start.ToString()))
            .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End.ToString()));

        CreateMap<AvailabilityForManipulationDto, Availability>()
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => TimeSpan.Parse(src.Start)))
            .ForMember(dest => dest.End, opt => opt.MapFrom(src => TimeSpan.Parse(src.End)));

        CreateMap<Availability, AvailabilityForUpdateDto>().ReverseMap();
    }
}