using System.ComponentModel.DataAnnotations;

namespace CLIQ_UE.ViewModels
{
	public class RegisterViewModel
	{
		[DataType(dataType: DataType.EmailAddress, ErrorMessage = "Enter Valid Email")]
		public string Email { get; set; }

		[DataType(dataType: DataType.Password)]
		public string Password { get; set; }
		[DataType(dataType: DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and Confirm Password not Match")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Plese Chack")]
		public bool AgreeWithServices { get; set; }

	}
}
