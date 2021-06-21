using System;
using Unis.Domain;

namespace Unis.Repository
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        public User GetUser(string token);
    }
}
