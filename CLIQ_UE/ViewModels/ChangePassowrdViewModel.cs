using System.ComponentModel.DataAnnotations;

namespace CLIQ_UE.ViewModels
{
    public class ChangePassowrdViewModel
    {
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Enter Complex Password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password and Confirm Password not Match")]
        public string ConfirmNewPassword { get; set; }
    }


}
