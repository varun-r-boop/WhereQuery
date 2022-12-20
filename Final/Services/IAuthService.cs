using Final.Model.Auth;
using System.Threading.Tasks;

namespace Final.Services
{
    public interface IAuthService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        void Register(RegisterRequest request);
        Task ForgotPassword(ForgotPasswordRequest request);
        Task ResetPassword(ResetPasswordRequest request);
        Task Verify(string token);

        Task Logout(LogoutRequest request);
    }

}
