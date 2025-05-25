using DataAcccess.DTO;
using DataAcccess.RequestData;

namespace DataAcccess.IServices
{
    public interface IUserService
    {
        Task<LoginDTO> Login(LoginRequestData loginRequestData);
        Task<int> UpdateRefreshToken(UserDTO userDTO);
        Task<UserDTO> GetByUserName(string userName);
    }
}
