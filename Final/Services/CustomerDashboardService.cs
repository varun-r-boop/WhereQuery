using AutoMapper;
using AutoMapper.QueryableExtensions;
using Final.Authorization;
using Final.Data;
using Final.Entities;
using Final.Model.CustomerDashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Final.Services
{
    public class CustomerDashboardService : ICustomerDashboardService
    {
        private HomezillaContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private IHostingEnvironment _environment;

        public CustomerDashboardService(
            HomezillaContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IHostingEnvironment environment)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _environment = environment;
        }

        //Get Customer Details

        public async Task<CustomerDetailsRequest> GetById(Guid id)
        {
            var user = await _context.customer
                .SingleOrDefaultAsync(x => x.CustomerUserID == id);
            if (user == null)
                throw new KeyNotFoundException("User Not Found");

            return _mapper.Map<CustomerDetailsRequest>(user);
        }

        //Update Customer Profile

        public async Task UpdateCustomerProfile(CustomerDetailsRequest request ,Guid id)
        {
            var user = GetUserId(id);
            _mapper.Map(request,user);
            _context.customer.Update(user);
            await _context.SaveChangesAsync();
           
        }

        //ChangePassword

        public async Task ChangePassword(ChangePasswordRequest request)
        {
            var user = GetUserEmail(request.email);
            if (user == null)
            {
                throw new BadHttpRequestException("email not found");
            }
                 
            user.passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _context.authentications.Update(user);
            await _context.SaveChangesAsync();
        }

        //Display Current Orders
        public async Task<IEnumerable<OrderDetailsResponse>> GetCurrentOrders(Guid id)
       {
            return await _context.orderDetails
                .Where(x=> x.Id == id && (x.orderStatus == orderStatus.Waiting || x.orderStatus == orderStatus.Accepted))
                .ProjectTo<OrderDetailsResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        //Display Past Orders
        public async Task<IEnumerable<OrderDetailsResponse>> GetPastOrders(Guid id)
        {
            return await _context.orderDetails
                .Where(x=> x.Id == id && (x.orderStatus == orderStatus.Delivered ||
                x.orderStatus == orderStatus.Declined ||
                x.orderStatus == orderStatus.NoResponse))
                .ProjectTo<OrderDetailsResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        private Authentication GetUserEmail(string Email)
        {
            return _context.authentications.SingleOrDefault(x => x.Email == Email);  
        }

        //Upload Profile Picture
      /*  public async Task<ProfilePicture> UploadProfilePicture(IFormFile file,Guid id)
        {

            var ImagePath = await Path.Combine(_environment.WebRootPath, "images" ,file.Name);
            using (var fileStream = new FileStream(ImagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var user = await _context.customer.SingleOrDefaultAsync(x => x.Id== id);
            user.ProfilePicture.ImagePath = "/images" + file.FileName;
            _context.SaveChanges();
            return null;
        }
*/        //Get UserID

        private Customer GetUserId(Guid id)
        {
            return _context.customer.SingleOrDefault(x => x.CustomerUserID == id);
        }


    }
}
