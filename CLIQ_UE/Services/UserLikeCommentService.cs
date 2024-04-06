using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class UserLikeCommentService : IUserLikeCommentService
    {
        private readonly IUserLikeCommentRepository _userLikeCommentRepository;
        private readonly ICommentRepository _commentRepository;

        public UserLikeCommentService(IUserLikeCommentRepository repository
            , ICommentRepository commentRepository)
        {
            _userLikeCommentRepository = repository;
            _commentRepository = commentRepository;
        }

        public bool LikeComment(UserLikeComment like)
        {
            
            Comment? comment = _commentRepository.GetCommentById(like.CommentId);
            UserLikeComment? userLikeCommentFromDb = _userLikeCommentRepository.Get(like);
            if(comment != null)
            {
                if(userLikeCommentFromDb == null)
                {
                    comment.LikeCount += 1;
                    _commentRepository.UpdateComment(comment);
                    _userLikeCommentRepository.LikeComment(like!);
                }
                else
                {
                    comment.LikeCount -= 1;
                    _commentRepository.UpdateComment(comment);
                    _userLikeCommentRepository.UnLikeComment(userLikeCommentFromDb!);
                }
                return true;
            }
            return false;
        }
    }
}
