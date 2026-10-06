using AutoMapper;
using ClientManagementAPI.DTOs;
using ClientManagementAPI.Models;

namespace ClientManagementAPI.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientReadDto>();
            CreateMap<ClientCreateDto, Client>();
        }
    }
}
