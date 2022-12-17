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
            CreateMap<Customer, AuthenticateResponse>();

            // RegisterRequest -> Customer
            CreateMap<RegisterRequest, Customer>();

            CreateMap<CustomerDetailsRequest, Customer>();

            CreateMap<orderDetails, OrderDetailsResponse>();

        }
    }
}
