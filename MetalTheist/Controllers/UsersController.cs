using MetalTheist.Data.Entities;
using MetalTheist.Data.Extensions;
using MetalTheist.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalTheist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly LinkGenerator linkGenerator;

        public UsersController(IUserRepository userRepository, LinkGenerator linkGenerator)
        {
            this.userRepository = userRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet] //Gets all the Users from the database
        public async Task<ActionResult<List<User>>> Get(bool includeBands=false, bool includeArticles=false)
        {
            try
            {
                var results = await userRepository.GetAllUsersAsync(includeBands, includeArticles);

                return results.OrderBy(r => r.Id).ToList();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpGet("{id:int}")] //Gets the User with id "id" from the database
        public async Task<ActionResult<User>> GetUserById(int id, bool includeBands=false, bool includeArticles=false)
        {
            try
            {
                var result = await userRepository.GetUserAsyncById(id, includeBands, includeArticles);
                if (result == null) return NotFound($"There is no User with id {id}");

                return result;
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpPost] //Adds a User to the Users' table
        public async Task<ActionResult<User>> Post(User model, bool includeBands=false, bool includeArticles=false)
        {
            try
            {
                var existing = await userRepository.GetUserAsyncById(model.Id, includeBands, includeArticles);
                if (existing != null) return BadRequest($"There is already a user with id: {model.Id}");

                var url = linkGenerator.GetPathByAction("Get", "Users", new { id = model.Id });
                if (string.IsNullOrWhiteSpace(url))
                {
                    return BadRequest("Could not use current id");
                }

                userRepository.Add(model);

                if (await userRepository.CommitAsync())
                {
                    return Created(url, model);
                }
                else
                {
                    return BadRequest("Failed to create new User");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
        }

        [HttpPut("{id:int}")] //Updates a User
        public async Task<ActionResult<User>> Put(int id, User model, bool includeBands=false, bool includeArticles=false)
        {
            try
            {
                var user = await userRepository.GetUserAsyncById(id);
                if (user == null) return NotFound($"Could not find Band with id of {id}");

                user.Map(model);

                if (await userRepository.CommitAsync())
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest("Failed to update User");
        }

        [HttpDelete("{id:int}")] //Deletes a User
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = await userRepository.GetUserAsyncById(id);
                if (existing == null) return NotFound($"Could not find Band with id of {id}");

                userRepository.Delete(existing);

                if (await userRepository.CommitAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure: " + ex.Message);
            }
            return BadRequest("Failed to delete User");
        }

    }
}
