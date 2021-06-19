using System;
using System.Threading.Tasks;
using Unis.Repository;
using System.Linq;

namespace Unis.API
{
    public class UserBL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public UserBL(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this._unitOfWork = unitOfWork;
            this._userRepository = userRepository;
        }

        public async Task<ServiceResult> Login(string username, string password)
        {
            var serviceResult = new ServiceResult();
            var user = this._userRepository.List(x =>  x.UserName == username && x.Password == password).FirstOrDefault();
            serviceResult.Data = user;
            return serviceResult;
        }
    }
}
