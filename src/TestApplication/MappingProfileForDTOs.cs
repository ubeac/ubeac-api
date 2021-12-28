using AutoMapper;
using uBeac.Identity;

namespace uBeac.Web.Identity
{
    public class MappingProfileForDTOs: Profile
    {
        public MappingProfileForDTOs()
        {
            CreateMap<RegisterRequest, User<Guid>>();
            CreateMap<TokenResult<Guid, User<Guid>>, LoginResponse<Guid>>();
        }
    }
}
