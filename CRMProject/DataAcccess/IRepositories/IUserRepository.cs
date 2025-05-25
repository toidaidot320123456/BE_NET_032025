using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.RequestData;

namespace DataAcccess.IRepositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<LoginDTO> Login(LoginRequestData loginRequestData);
        Task<int> UpdateRefreshToken(UserDTO userDTO);
        Task<UserDTO> GetByUserName(string userName);
    }
}
