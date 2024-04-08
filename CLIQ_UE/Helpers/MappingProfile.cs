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

            CreateMap<BookMark,BookMarkViewModel>();
            CreateMap<BookMarkViewModel, BookMark>();

            /*CreateMap<Comment, RespCommentVM>()
				.ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User!.FirstName))
				.ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User!.LastName))
				.ForMember(dest => dest.UserProfileImage, opt => opt.MapFrom(src => src.User!.ProfileImage));*/

            CreateMap<Comment, RespCommentVM>()
    .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User!.FirstName))
    .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User!.LastName))
    .ForMember(dest => dest.UserProfileImage, opt => opt.MapFrom(src => src.User!.ProfileImage))
    .ForMember(dest => dest.IsLikedByMe, opt => opt.MapFrom(src => src.UserLikeComments != null && src.UserLikeComments.Count > 0));

        }
    }
}
