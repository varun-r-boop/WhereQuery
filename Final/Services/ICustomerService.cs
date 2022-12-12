using Final.Model.Customers;

namespace Final.Services
{
    public interface ICustomerService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<Customer> GetAll();
        Customer GetById(Guid id);
        void Register(RegisterRequest model);
        void Update(Guid id, UpdateRequest model);
        void Delete(Guid id);
    }

}
