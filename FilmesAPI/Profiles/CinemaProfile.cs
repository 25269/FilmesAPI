using AutoMapper;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Models;

namespace FilmsAPI.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDTO, Cinema>();
            CreateMap<Cinema, UpdateCinemaDTO>();
            CreateMap<Cinema,ReadCinemaDTO>().ForMember(cinemaDTO => cinemaDTO.Address,
                    opt => opt.MapFrom(cinema => cinema.Address));
            CreateMap<Cinema, ReadCinemaDTO>().ForMember(cinemaDTO => cinemaDTO.Sessions,
                    opt => opt.MapFrom(cinema => cinema.Sessions));
        }
    }
}
