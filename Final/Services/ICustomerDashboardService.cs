using Final.Model.CustomerDashboard;

namespace Final.Services
{
    public interface ICustomerDashboardService
    {
        Task ChangePassword(ChangePasswordRequest request);
        Task<CustomerDetailsRequest> GetById(Guid id);
        Task UpdateCustomerProfile(CustomerDetailsRequest request, Guid id);
        Task<IEnumerable<OrderDetailsResponse>> GetCurrentOrders(Guid id);
        Task<IEnumerable<OrderDetailsResponse>> GetPastOrders(Guid id);
    }
}
