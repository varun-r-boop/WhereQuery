using AutoMapper;
using Final.Model.Customers;

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

            // UpdateRequest -> User
            CreateMap<UpdateRequest, Customer>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}
