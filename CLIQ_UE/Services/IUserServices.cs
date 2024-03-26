using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Services
{
	public interface IUserServices
	{
		public ApplicationUser MapRegisterViewModelToAppUser(RegisterViewModel viewModel);
	}
}
