using AutoMapper;
using PersonManagementAPI.DTOs;
using PersonManagementAPI.Models;

namespace PersonManagementAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();

            CreateMap<PhoneNumber, PhoneNumberDTO>();
            CreateMap<PhoneNumberDTO, PhoneNumber>();

            CreateMap<ConnectedPerson, ConnectedPersonDTO>();
            CreateMap<ConnectedPersonDTO, ConnectedPerson>();
        }
    }
}
