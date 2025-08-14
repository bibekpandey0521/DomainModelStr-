using AutoMapper;
using BZWalks.API.Models.Domain;
using BZWalks.API.Models.DTO;

namespace BZWalks.API.Mappings
{
    public class AutoMapperProfiles :Profile
    {
        //public AutoMapperProfiles()
        //{
        //    //CreateMap<UserDTO, UserDomain>();
        //    //CreateMap<UserDTO, UserDomain>().ReverseMap();
        //    //CreateMap<UserDTO, UserDomain>()
        //    //    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
        //    //    .ReverseMap();
        //}

        //public class UserDTO
        //{
        //    public string FullName { get; set; }
        //}

        //public class UserDomain
        //{
        //    //public string FullName { get; set; }
        //    public string Name { get; set; }
        //}

        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,RegionDto>().ReverseMap();
        }
    }
}
