using DataAcccess.DTO;
using DataAcccess.IRepositories;
using DataAcccess.IServices;
using DataAcccess.RequestData;

namespace DataAcccess.Services
{
    public class UserService : IUserService
    {
        private IProductRepository _productRepository;
        private IUserRepository _userRepository;
        public UserService(IProductRepository productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        public async Task<LoginDTO> Login(LoginRequestData loginRequestData)
        {
            return await _userRepository.Login(loginRequestData);
        }
        public async Task<int> UpdateRefreshToken(UserDTO userDTO)
        {
            return await _userRepository.UpdateRefreshToken(userDTO);
        }
        public async Task<UserDTO> GetByUserName(string userName)
        {
            return await _userRepository.GetByUserName(userName);
        }
    }
}
