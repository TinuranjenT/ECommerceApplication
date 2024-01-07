using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Extensions.Logging;

namespace EcommerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository _Repository;
        public UserController(IRepository Repository)
        {
            _Repository = Repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<User> users = _Repository.GetUsers();
                if (users == null)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting users", ex);
            }

        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            try
            {
                User user = _Repository.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting user by ID", ex);
            }
        }

        [HttpGet("credentials")]
        [Produces("application/xml")]
        public IActionResult GetUserCredentials()
        {
            try
            {
                var userCredentialList = _Repository.GetUserCredentials();

                var user = new List<UserDto>();
                foreach (var userCredential in userCredentialList)
                {
                    user.Add(new UserDto
                    {
                        userName = userCredential.userName,
                        passWord = userCredential.passWord
                    });

                }
                //Log.Information("User credentials: {@UserCredentials}", userCredentialList);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");

                throw new Exception("Error getting user credentials from XML", ex);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                _Repository.CreateUser(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating user", ex);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, User updatedUser)
        {
            try
            {
                _Repository.UpdateUser(id, updatedUser);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user", ex);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _Repository.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user", ex);
            }

        }



    }
}