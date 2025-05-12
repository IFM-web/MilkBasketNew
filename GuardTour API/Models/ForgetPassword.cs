using Microsoft.OpenApi.Attributes;

namespace GuardTour_API.Models
{
    public class ForgetPassword
    {
        
        public string? EmployeeCode { get; set; }
        public string? OldPassword { get; set; }
        public string? ConfirmPassword { get; set; }

    }
}
