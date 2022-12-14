using AutoMapper;
using Final.Authorization;
using Final.Data;
using Final.Helpers;
using Final.Model.Auth;
using Final.Model.Customers;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using Stripe;

namespace Final.Services
{
    
    public class AuthService : IAuthService
    {
        private HomezillaContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public AuthService(
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
            var user = _context.customer.SingleOrDefault(x => x.email == model.email);

            if(user == null || !user.isVerified || !BCrypt.Net.BCrypt.Verify(model.password,user.passwordHash) )
            {
                throw new BadHttpRequestException("Credentials are incorrect");
            }
            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }


        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.customer.Any(x => x.email == model.email))
                throw new AppException("Email '" + model.email + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<Customer>(model);
            user.userRole = Entities.Role.customer;

            user.createdAt= DateTime.Now;
           // user.verificationToken = _jwtUtils.GenerateToken(user);

            // hash password
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            user.passwordHash = BCrypt.Net.BCrypt.HashPassword(model.password, salt);

            // send verification mail

            user.OTP = GenerateOtp();
            user.OTPExpiresAt = DateTime.Now.AddMinutes(30);
            user.verifiedAt= DateTime.MinValue;
            // save user
            _context.customer.Add(user);
            _context.SaveChanges();
        }
    
      

       
        //verification
        public async Task Verify(string otp)
        {
            var user = _context.customer.SingleOrDefault(x => x.OTP== otp);
            if (user == null) throw new KeyNotFoundException("Invalid verification OTP");
            if(user.verifiedAt != DateTime.MinValue)
            {
                throw new KeyNotFoundException("User already verified");
            }
            user.verifiedAt = DateTime.Now;
            user.isVerified= true;
            user.OTP = null;
            user.OTPExpiresAt= DateTime.MinValue;

            await _context.SaveChangesAsync();
        }

        //forgot password 

        public async Task ForgotPassword(ForgotPasswordRequest request)
        {
            var user = _context.customer.SingleOrDefault(
                x => (x.email == request.email) && !x.isDeleted);

            if (user == null) throw new BadHttpRequestException("User not found");

            user.OTP = GenerateOtp();
            user.OTPExpiresAt = DateTime.Now.AddMinutes(5);
            _context.customer.Update(user);
            await _context.SaveChangesAsync();
        }

        //reset password

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            var user = GetUserByOtp(request.Otp);
            if(user == null || user.OTPExpiresAt < DateTime.Now)
            {
                throw new BadHttpRequestException("Invalid OTP");
            }
            user.passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);
            user.passwordResetAt = DateTime.Now;
            user.OTP = null;
            user.OTPExpiresAt = DateTime.MinValue;

            _context.customer.Update(user);
            await _context.SaveChangesAsync();

        }

        //logout

        public async Task Logout(LogoutRequest request)
        {
            request.Token = null;
            await _context.SaveChangesAsync();
        }

        private Customer GetUserByOtp(string otp)
        {
            return _context.customer.SingleOrDefault(x => x.OTP == otp);
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }
    }
}
