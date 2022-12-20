using AutoMapper;
using Final.Entities;
using Final.Model.Auth;
using Final.Model.CustomerDashboard;

namespace Final.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Customer -> AuthenticateResponse
            CreateMap<Authentication, AuthenticateResponse>();

            // RegisterRequest -> Customer
            CreateMap<RegisterRequest, Authentication>();

            CreateMap<Customer, CustomerDetailsRequest>();
            CreateMap<CustomerDetailsRequest, Customer>();

            CreateMap<orderDetails, OrderDetailsResponse>();

        }
    }
}
