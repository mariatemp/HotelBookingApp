using AutoMapper;
using HotelBookingApp.DTO;
using HotelBookingApp.Models;

namespace HotelBookingApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Accommodation, AccommodationDTO>().ReverseMap();
            CreateMap<Booking, BookingDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
          //  CreateMap<RegisterDTO, User>().ReverseMap();
          //  CreateMap<LoginDTO, User>().ReverseMap();
        }

    }
}
