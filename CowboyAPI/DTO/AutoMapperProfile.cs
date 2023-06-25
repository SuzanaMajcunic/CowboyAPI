using AutoMapper;
using Cowboy.Repository.Entities;
using Cowboy.Repository.Models;

namespace Cowboy.API.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CowboyEntity, CowboyModel>();
            CreateMap<FirearmEntity, FirearmModel>();
        }
    }
}
