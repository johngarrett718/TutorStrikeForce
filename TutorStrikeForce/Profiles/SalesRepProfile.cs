using AutoMapper;
using TutorStrikeForce.Models;
using TutorStrikeForce.ViewModels;

namespace TutorStrikeForce.Profiles
{
    public class SalesRepProfile : Profile
    {
        public SalesRepProfile()
        {
            CreateMap<SalesRep, SalesRepEditModel>();
            CreateMap<SalesRepEditModel, SalesRep>();
        }
    }
}