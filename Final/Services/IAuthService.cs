using Final.Model.Auth;

namespace Final.Services
{
    public interface IAuthService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task Register(RegisterRequest model);
        Task ForgotPassword(ForgotPasswordRequest request);
        Task ResetPassword(ResetPasswordRequest request);
        Task Verify(string token);

        Task Logout(LogoutRequest request);
    }

}
