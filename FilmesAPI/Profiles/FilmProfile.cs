using AutoMapper;
using FilmesAPI.Models;
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
            CreateMap<Film, ReadFilmDTO>();
        }
    }
}
