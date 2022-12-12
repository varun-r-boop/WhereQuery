using AutoMapper;
using Final.Authorization;
using Final.Data;
using Final.Helpers;
using Final.Model.Customers;
using Org.BouncyCastle.Crypto.Generators;
using Stripe;

namespace Final.Services
{
    
    public class CustomerService : ICustomerService
    {
        private HomezillaContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public CustomerService(
            HomezillaContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.customer.SingleOrDefault(x => x.userName == model.userName);

            // validate
            //if (user == null || !BCrypt.Verify(model.password, user.passwordHash))
               // throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.customer;
        }

        public Customer GetById(Guid id)
        {
            return getUser(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.customer.Any(x => x.userName == model.userName))
                throw new AppException("Username '" + model.userName + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<Customer>(model);

            // hash password
           // user.passwordHash = BCrypt.HashPassword(model.password);

            // save user
            _context.customer.Add(user);
            _context.SaveChanges();
        }

        public void Update(Guid id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate
            if (model.userName != user.userName && _context.customer.Any(x => x.userName == model.userName))
                throw new AppException("Username '" + model.userName + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.password))
               // user.passwordHash = BCrypt.HashPassword(model.password);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.customer.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = getUser(id);
            _context.customer.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private Customer getUser(Guid id)
        {
            var user = _context.customer.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
