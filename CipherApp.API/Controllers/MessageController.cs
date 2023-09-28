using CipherApp.BLL.Services.IServices;
using CipherApp.BLL.Utilities.CustomExceptions;
using CipherApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CipherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _services;

        public MessageController(IMessageService services)
        {
            _services = services;
        }

        private IActionResult BadRequestResponse() =>
            BadRequest("Something went wrong");

        private IActionResult NotFoundResponse(int id) =>
            NotFound($"Unable to find a message with the id = {id}");

        public async Task<IActionResult> GetMessages()
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

        public async Task<IActionResult> GetMessage(int id)
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

        public async Task<IActionResult> CreateMessage(Message message)
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

        public async Task<IActionResult> UpdateMessage(Message message)
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

        public async Task<IActionResult> DeleteMessage()
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
