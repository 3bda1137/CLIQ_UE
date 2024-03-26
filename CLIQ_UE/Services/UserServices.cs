using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
	public class UserServices : IUserServices
	{
		public ApplicationUser MapRegisterViewModelToAppUser(RegisterViewModel viewModel)
		{
			ApplicationUser applicationUser = new ApplicationUser();
			applicationUser.Email = viewModel.Email;
			applicationUser.PasswordHash = viewModel.Password;
			//??????
			applicationUser.UserName = GenerateUsernameFromEmail(viewModel.Email);
			return applicationUser;
		}

		private string GenerateUsernameFromEmail(string email)
		{
			int atIndex = email.IndexOf('@');
			string username;
			if (atIndex >= 0)
			{
				username = email.Substring(0, atIndex);
			}
			else
			{
				username = email;
			}
			return username;
		}
	}
}
