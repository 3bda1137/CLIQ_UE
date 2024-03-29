﻿using CLIQ_UE.Models;

namespace CLIQ_UE.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContext context;

        public CommentRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public void AddComment(Comment comment)
        {
            context.Comments.AddAsync(comment);
            context.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByPost(int postId)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
