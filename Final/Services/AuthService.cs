using AutoMapper;
using Final.Authorization;
using Final.Data;
using Final.Helpers;
using Final.Model.Auth;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using Stripe;
using Final.Entities;
using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Final.MailServices;


namespace Final.Services
{

    public class AuthService : IAuthService
    {
        private HomezillaContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private Authentication _user = new();
        private readonly IMailService _mailer;
        private static MailTemplates mailTemplate = new MailTemplates();

        public AuthService(
            HomezillaContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IMailService mailer)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _mailer = mailer;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.authentications.SingleOrDefault(x => x.Email == model.email);

            if(user == null || !user.isVerified || !BCrypt.Net.BCrypt.Verify(model.password,user.passwordHash) )
            {
                throw new BadHttpRequestException("Credentials are incorrect");
            }
            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }


        public void Register(RegisterRequest request)
        {
            // validate
           
                
            if(! _context.authentications.Any(x => x.Email == request.email))
            {
                _user = _mapper.Map<Authentication>(request);
                
                _user.createdAt = DateTime.Now;
                // user.verificationToken = _jwtUtils.GenerateToken(user);

                // hash password
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                _user.passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password, salt);

                _user.OTP = GenerateOtp();
                _user.OTPExpiresAt = DateTime.Now.AddMinutes(30);
                _user.verifiedAt = DateTime.MinValue;

             _context.authentications.Add(_user);
             _context.SaveChanges();
            
           
            if (request.UserRole == "customer")
                {
                    Customer customer = new();
               
                var res = _context.authentications.Where(x => x.Email == request.email).FirstOrDefault();
                
                    customer.CustomerUserID= res.AuthId;
                    customer.firstName = request.firstName;
                    customer.lastName = request.lastName;
                    customer.email = request.email;
                    customer.userName= request.userName;
                  _context.customer.Add(customer);
                    _context.SaveChanges();


                }
                else
                {
                    providerDetails provider = new();
                  var res = _context.authentications.Where(x => x.Email == request.email).FirstOrDefault();
                    provider.ProviderUserID= res.AuthId;
                    provider.ProviderFirstName = request.firstName;
                    provider.ProviderLastName = request.lastName;
                provider.ProviderEmail = request.email;
                    _context.providerDetails.Add(provider);
                    _context.SaveChanges();


                }
            

             }
            else
            {
                throw new KeyNotFoundException("Email already registered");
            }
             


        }
    
      

       
        //verification
        public async Task Verify(string otp)
        {
            var user = _context.authentications.SingleOrDefault(x => x.OTP== otp);
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
            var user = _context.authentications.SingleOrDefault(
                x => (x.Email == request.email) && !x.isDeleted);

            if (user == null) throw new BadHttpRequestException("User not found");

            user.OTP = GenerateOtp();
            Console.WriteLine(user.OTP);
            user.OTPExpiresAt = DateTime.Now.AddMinutes(5);
            Console.WriteLine(user.OTPExpiresAt);
            string template = mailTemplate.GetPasswordResetTemplate(user.OTP, 10);
            await _mailer.Send(user.Email, "Password Reset Email Send", template);
            Console.WriteLine("jjjjjjjjjjj");
            _context.authentications.Update(user);
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

            _context.authentications.Update(user);
            await _context.SaveChangesAsync();

        }

        //logout

        public async Task Logout(LogoutRequest request)
        {
            request.Token = null;
            await _context.SaveChangesAsync();
        }

        //OTP Generation

        private Authentication GetUserByOtp(string otp)
        {
            return _context.authentications.SingleOrDefault(x => x.OTP == otp);
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }

        public async Task<Authentication> GetUser(string email ="")
        {
            Authentication? result = new();
            if(email != "")
            {
                result = await _context.authentications.Where(u => u.Email == email).FirstOrDefaultAsync();
            }
            return result!;
        }
        
        }
    }

