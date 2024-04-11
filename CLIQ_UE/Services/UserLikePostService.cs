using CLIQ_UE.Models;
using CLIQ_UE.Repositories;

namespace CLIQ_UE.Services
{
    public class UserLikePostService : IUserLikePostService
    {
        private readonly IUserLikePostRepository _repository;

        public UserLikePostService(IUserLikePostRepository repository)
        {
            _repository = repository;
        }

        public UserLikePost? Get(int postId, string UID)
        {
            return _repository.Get(postId, UID);
        }

        public UserLikePost Update(UserLikePost user)
        {
            return  _repository.Update(user);
        }

        public UserLikePost Add(UserLikePost user)
        {
            return _repository.Add(user);
        }

        public void Delete(UserLikePost likePost)
        {
            _repository.Delete(likePost);
        }
    }
}
