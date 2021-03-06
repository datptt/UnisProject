using System;
using Unis.Domain;

namespace Unis.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public User GetUser(string token)
        {
            return new User()
            {
                UserName = "ptdat"
            };
        }

    }
}
