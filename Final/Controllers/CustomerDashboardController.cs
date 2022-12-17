using AutoMapper;
using Final.Model.CustomerDashboard;
using Final.Services;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDashboardController : ControllerBase
    {

        private ICustomerDashboardService _customerDashboardService;
        private IMapper _mapper;
        public CustomerDashboardController(
            ICustomerDashboardService customerDashboardService,
            IMapper mapper)
        {
            _customerDashboardService = customerDashboardService;
            _mapper = mapper;
        }
        [HttpGet("get-users-by-id")]
        public async Task<ActionResult<CustomerDetailsRequest>> GetById(Guid id)
        {
           var user =   await _customerDashboardService.GetById(id);
            return Ok(user);
        }

        [HttpPut("update-customer-profile")]
        public async Task<OkObjectResult> UpdateCustomerProfile(CustomerDetailsRequest request, Guid id)
        {
            await _customerDashboardService.UpdateCustomerProfile(request, id);
            return Ok(request);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                await _customerDashboardService.ChangePassword(request);
                return Ok(new { message = "Your Password has been changed successfully" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get-current-orders")]
        public async Task<ActionResult<IEnumerable<OrderDetailsResponse>>> GetCurrentOrders(Guid id )
        {
            return Ok( await _customerDashboardService.GetCurrentOrders(id));
            
        }

        [HttpGet("Get-past-orders")]
        public async Task<ActionResult<IEnumerable<OrderDetailsResponse>>> GetPastOrders(Guid id)
        {
            return Ok(await _customerDashboardService.GetPastOrders(id));

        }
    }
}
