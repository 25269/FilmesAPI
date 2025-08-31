using AutoMapper;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Models;

namespace FilmsAPI.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressDTO, Address>();
            CreateMap<Address, UpdateAddressDTO>();
            CreateMap<Address, ReadAddressDTO>();
        }
    }
}
