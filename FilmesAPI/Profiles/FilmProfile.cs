using AutoMapper;
using FilmsAPI.Models;
using FilmsAPI.Data.DTOs;

namespace FilmsAPI.Profiles
{
    public class FilmProfile : Profile
    {
        public FilmProfile() 
        {
            CreateMap<CreateFilmDTO, Film>();
            CreateMap<UpdateFilmDTO, Film>();
            CreateMap<Film, UpdateFilmDTO>();
            CreateMap<Film, ReadFilmDTO>().ForMember(filmDTO => filmDTO.Sessions,
                    opt => opt.MapFrom(film => film.Sessions));

        }
    }
}
