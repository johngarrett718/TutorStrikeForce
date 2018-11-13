using AutoMapper;
using TutorStrikeForce.Models;
using TutorStrikeForce.ViewModels;

namespace TutorStrikeForce.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientEditModel>();
            CreateMap<ClientEditModel, Client>();
        }
    }
}
