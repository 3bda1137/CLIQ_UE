using System.ComponentModel.DataAnnotations;

namespace CLIQ_UE.ViewModels
{
	public class ResetPasswordViewModel
	{
		[DataType(dataType: DataType.Password)]
		public string Password { get; set; }


		[DataType(dataType: DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and Confirm Password not Match")]
		public string ConfirmPassword { get; set; }
	}
}
