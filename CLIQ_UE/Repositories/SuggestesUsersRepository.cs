using CLIQ_UE.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CLIQ_UE.Repositories
{
    public class SuggestesUsersRepository : ISuggestesUsersRepository
    {
        private readonly ApplicationContext context;

        public SuggestesUsersRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public List<ApplicationUser> GetSuggestesUsers(string userId)
        {
            ///  To get only the users that i'm not following

            string sqlQuery = @"
                SELECT TOP 3 u.*
                FROM AspNetUsers u
                LEFT JOIN Followers f ON u.Id = f.FollowingId AND f.FollowerId = @CurrentUserId
                WHERE u.Id != @CurrentUserId AND f.Id IS NULL
                ORDER BY NEWID();
            ";


            //string sqlQuery = @"
            //    SELECT TOP 3 u.*
            //    FROM AspNetUsers u
            //    ORDER BY NEWID();
            //";


            List<ApplicationUser> randomUsers = context.Users.FromSqlRaw(sqlQuery, new SqlParameter("@CurrentUserId", userId)).ToList();

            return randomUsers;
        }
    }
}
