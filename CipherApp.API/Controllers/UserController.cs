using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CipherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _services;

        public UserController(IUserService services)
        {
            _services = services;
        }

        private IActionResult BadRequestResponse() =>
            BadRequest("Something went wrong");

        private IActionResult NotFoundResponse(int id) =>
            NotFound($"Unable to find a user with the id = {id}");


        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequestResponse();
            }
        }

        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFoundResponse(id);
            }
            catch (Exception)
            {
                return BadRequestResponse();
            }
        }

        public async Task<IActionResult> CreateUser(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequestResponse();
            }
        }

        public async Task<IActionResult> UpdateUser(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequestResponse();
            }
        }

        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequestResponse();
            }
        }
    }
}
