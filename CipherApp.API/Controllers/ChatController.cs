using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CipherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _services;

        public ChatController(IChatService services)
        {
            _services = services;
        }

        private IActionResult BadRequestResponse() =>
            BadRequest("Something went wrong");

        private IActionResult NotFoundResponse(int id) =>
            NotFound($"Unable to find a chat with the id = {id}");

        public async Task<IActionResult> GetChats()
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

        public async Task<IActionResult> GetChat(int id)
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

        public async Task<IActionResult> CreateChat(Chat chat)
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

        public async Task<IActionResult> UpdateChat(Chat chat)
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

        public async Task<IActionResult> DeleteChat()
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
