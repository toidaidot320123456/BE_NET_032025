using DataAcccess.Common;
using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.IRepositories;
using DataAcccess.RequestData;

namespace DataAcccess.Repositories
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(BE_NET_032025Context context) : base(context)
        {
            this._set = context.Users;
        }
        public async Task<LoginDTO> Login(LoginRequestData loginRequestData)
        {
            //loginRequestData.Password = Sercurity.ComputeSha256Hash(loginRequestData.Password);
            var result = this._set.Where(x => x.UserName == loginRequestData.UserName && x.Password == loginRequestData.Password).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            LoginDTO loginDTO = new LoginDTO()
            {
                Id = result.Id,
                IsAdmin = result.IsAdmin.Value,
                UserName = result.UserName,
            };
            return loginDTO;
        }
        public async Task<int> UpdateRefreshToken(UserDTO userDTO)
        {
            var result = this._set.Where(x => x.UserName == userDTO.UserName).FirstOrDefault();
            if (result == null)
                return -1;
            else
            {
               result.RefreshToken = userDTO.RefreshToken;
               result.ExpriedTime = userDTO.ExpriedTime;
               return await this._context.SaveChangesAsync();
            }
        }
        public async Task<UserDTO> GetByUserName(string userName)
        {
            var result = this._set.Where(x => x.UserName == userName).FirstOrDefault();
            if (result == null)
                return null;
            UserDTO userDTO = new UserDTO()
            {
                Id = result.Id,
                IsAdmin = result.IsAdmin.Value,
                UserName = result.UserName,
                RefreshToken = result.RefreshToken,
                ExpriedTime = result.ExpriedTime.Value
            };
            return userDTO;
        }
    }
}
