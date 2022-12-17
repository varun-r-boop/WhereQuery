using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;

namespace Final.Model.CustomerDashboard
{
    public class ChangePasswordRequest
    {

        public string? email { get; set; }
        public string? OldPassword { get; set; }

        public string? NewPassword { get; set; }
        [Compare("NewPassword")]
        public string? ConfirmNewPassword { get; set; }

    }
}
