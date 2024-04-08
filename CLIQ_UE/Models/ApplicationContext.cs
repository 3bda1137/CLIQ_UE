using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CLIQ_UE.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationContext() : base() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        //DbSets
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<ChatIndividual> ChatIndividual { get; set; }
        public DbSet<OnlineUser> OnlineUsers { get; set; }

        public DbSet<Followers> Followers { get; set; }
        public virtual DbSet<LastMessage> LastMessages { get; set; }

        public DbSet<UserLikeComment> UserLikeComments { get; set; }
        public DbSet<BookMark> bookMarks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLikeComment>()
                .HasKey(x => new { x.CommentId, x.ApplicationUserId });


            //For comment's likes
            builder.Entity<UserLikeComment>()
            .HasOne(ulc => ulc.Comment)
            .WithMany(c => c.UserLikeComments)
            .HasForeignKey(ulc => ulc.CommentId);

            builder.Entity<UserLikeComment>()
                .HasOne(ulc => ulc.ApplicationUser)
                .WithMany(u => u.UserLikeComments)
                .HasForeignKey(ulc => ulc.ApplicationUserId);
        }

    }
}
