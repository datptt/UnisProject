using System;
using System.Threading.Tasks;
using Unis.Repository;
using System.Linq;
using Unis.Domain;

namespace Unis.API
{
    public class UserBL : BaseBL<User>
    {
        private readonly IUserRepository _userRepository;
        public UserBL(IUserRepository userRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._userRepository = userRepository;
        }

        public async Task<ServiceResult> Login(string username, string password)
        {
            var serviceResult = new ServiceResult();
            var user = this._userRepository.List(x => x.UserName == username && x.Password == password).FirstOrDefault();
            if (user != null)
            {
                var token = TokenHelper.CreateToken(user);
                serviceResult.IsSuccess = true;
                serviceResult.Data = token;
            }
            else
            {
                serviceResult.IsSuccess = false;
                serviceResult.ErrorMessage = "User or password invalid";
            }
            return serviceResult;
        }
    }
}
