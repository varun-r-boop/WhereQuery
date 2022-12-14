using AutoMapper;
using Final.Model.Auth;

namespace Final.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponse
            CreateMap<Customer, AuthenticateResponse>();

            // RegisterRequest -> User
            CreateMap<RegisterRequest, Customer>();

        }
    }
}
