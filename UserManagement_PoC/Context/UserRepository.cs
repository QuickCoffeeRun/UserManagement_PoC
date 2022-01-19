using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement_PoC.DTOs;
using UserManagement_PoC.Models;

namespace UserManagement_PoC.Context
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataContext _dataContext;
        public readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            IDataContext dataContext,
            IMapper mapper,
            ILogger<UserRepository> logger)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Add(CreateUserDto user)
        {
            var newUser = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone
            };

            _dataContext.Users.Add(newUser);

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add user to the database.", ex);
            }
        }

        public async Task AddBulk(List<CreateUserDto> users)
        {
            users.ForEach(user =>
            {
                _dataContext.Users.Add(_mapper.Map<CreateUserDto, User>(user));
            });

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add user bulk to the database.", ex);
            }
        }

        public async Task<UserDto> Get(int id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _dataContext.Users.ToListAsync();

            if (users == null)
                return null;

            return _mapper.Map<List<User>, IEnumerable<UserDto>>(users);
        }

        public async Task Update(int id, UpdateUserDto user)
        {
            var userToUpdate = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userToUpdate == null)
            {
                return;
            }

            userToUpdate.Name = user.Name;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Email = user.Email;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update user {id}.", ex);
            }
        }

        public async Task Delete(int id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return;
            }

            _dataContext.Users.Remove(user);

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to remove user {id}.", ex);
            }
        }
    }
}
