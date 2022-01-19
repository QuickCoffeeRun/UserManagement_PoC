using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement_PoC.DTOs;

namespace UserManagement_PoC.Context
{
    public interface IUserRepository
    {
        Task<UserDto> Get(int id);
        Task<IEnumerable<UserDto>> GetAll();
        Task Add(CreateUserDto user);
        Task AddBulk(List<CreateUserDto> users);
        Task Delete(int id);
        Task Update(int id, UpdateUserDto user);
    }
}
