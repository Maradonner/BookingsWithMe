using AutoMapper;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.Models.Availability;
using BookingsWithMe.Models.Booking;

namespace BookingsWithMe.Mapper
{
    public class BookingProfiler : Profile
    {
        public BookingProfiler()
        {
            CreateMap<Booking, BookingForCreationDto>().ReverseMap();

        }
    }
}
