using AutoMapper;
using CLIQ_UE.Models;
using CLIQ_UE.Repositories;
using CLIQ_UE.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CLIQ_UE.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper mapper;
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<ApplicationUser> userManager;
		public CommentService(ICommentRepository commentRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
		{
			this.commentRepository = commentRepository;
			this.mapper = mapper;
			this.userManager = userManager;
		}


		public async Task<bool> AddComment(AddCommentViewModel commentVM, ClaimsPrincipal User)
        {
            Comment comment = mapper.Map<Comment>(commentVM);
            comment.CommentDate = DateTime.Now;
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                comment.UserId = user.Id;
            }
            else
            {
                //No user, anonymous may be
                return false;
            }
            comment.LikeCount = 0;
            commentRepository.AddComment(comment);
            return true;
        }

        public void DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByPost(int postId)
        {
            return commentRepository.GetCommentsByPost(postId);
        }

        public void UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
