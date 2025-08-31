using AutoMapper;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Models;

namespace FilmsAPI.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<CreateSessionDTO, Session>();
            CreateMap<Session, ReadSessionDTO>();
        }

    }
}
