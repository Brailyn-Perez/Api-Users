using Api_Users.DAL.Daos;
using Api_Users.DAL.Entities;
using Api_Users.DAL.Interfaces;
using Api_Users.Models;
using Microsoft.AspNetCore.Mvc;
namespace Api_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDaoUsers _daoUsers;
        public UsersController(IDaoUsers daoUsers) 
        {
            _daoUsers = daoUsers;
        }
   
        [HttpGet("Usuarios")]
        public async Task<List<UserGetModel>> Get()
        {
            var users = await _daoUsers.GetAllAsync();
            List<UserGetModel> result = new List<UserGetModel>();
            foreach (var user in users) 
            {
                result.Add(new UserGetModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    CreationDate = user.CreationDate

                });
            }

            return result;
        }

        [HttpGet("Usuarios/{id}")]
        public async Task<UserGetModel> Get(int id)
        {
            var user = await _daoUsers.GetByIdAsync(id);

            var userGet = new UserGetModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                CreationDate = user.CreationDate

            };

            return userGet;
        }

        [HttpPost("Usuarios")]
        public async Task Post([FromBody] UserCreateModel user)
        {
            var userCreate = new Users()
            {
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            await _daoUsers.CreateAsync(userCreate);
        }

        [HttpPut("Usuarios/{id}")]
        public async Task Put(int id, [FromBody] UserUpdateModel user)
        {
            var userUpdate = new Users()
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            await _daoUsers.UpdateAsync(userUpdate);
        }

        
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _daoUsers.DeleteAsync(id);
        }
    }
}
