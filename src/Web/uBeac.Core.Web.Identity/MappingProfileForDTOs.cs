//using AutoMapper;
//using uBeac.Identity;

//namespace uBeac.Web.Identity
//{    
//    public class MappingProfileForDTOs<TUserKey, TUser, TRegisterRequest, TLoginRequest, TLoginResponse> : Profile
//       where TUserKey : IEquatable<TUserKey>
//       where TUser : User<TUserKey>
//       where TRegisterRequest : RegisterRequest
//       where TLoginRequest : LoginRequest
//       where TLoginResponse : LoginResponse<TUserKey>
//    {
//        public MappingProfileForDTOs()
//        {
//            CreateMap<TRegisterRequest, TUser>();
//            CreateMap<TokenResult<TUserKey, TUser>, TLoginResponse>();
//        }
//    }
//}
