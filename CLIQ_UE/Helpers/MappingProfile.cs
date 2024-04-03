using AutoMapper;
using CLIQ_UE.Models;
using CLIQ_UE.ViewModels;

namespace CLIQ_UE.Helpers
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			//////////TSource              TDestination
			CreateMap<AddCommentViewModel, Comment>();
			CreateMap<Comment, AddCommentViewModel>();

            CreateMap<LikeCommentVM, UserLikeComment>();
            CreateMap<UserLikeComment, LikeCommentVM>();

        }
    }
}
