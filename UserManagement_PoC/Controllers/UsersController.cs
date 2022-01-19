using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UserManagement_PoC.Context;
using UserManagement_PoC.DTOs;

namespace UserManagement_PoC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: /<UsersController>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _repository.GetAll();
        }

        // GET /<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDto> Get(int id)
        {
            return await _repository.Get(id);
        }

        // POST /<UsersController>
        [HttpPost]
        public async Task Post([FromBody] CreateUserDto value)
        {
            await _repository.Add(value);
        }

        // POST /<UsersController/FromFile>
        [Route("FromFile")]
        [HttpPost]
        public async Task FromFile([FromForm] IFormFile file)
        {
            var usersToAdd = new List<CreateUserDto>();

            using (StreamReader sr = new StreamReader(file.OpenReadStream()))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    var columns = line.Split('\u002C');

                    usersToAdd.Add(new CreateUserDto(columns[0], columns[1], columns[2], columns[3]));
                }
            }

            await _repository.AddBulk(usersToAdd);
        }

        // PUT /<UsersController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UpdateUserDto value)
        {
            await _repository.Update(id, value);
        }

        // DELETE /<UsersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
