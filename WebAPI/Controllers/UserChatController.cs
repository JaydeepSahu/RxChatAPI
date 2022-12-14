using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Service_Layer.CustomServices;
using Service_Layer.ICustomServices;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserChatController : ControllerBase
    {
        private readonly IUserChatService _userChat;
        private readonly IStringLocalizer<UserChatController> _stringLocalizer;
        public UserChatController(IUserChatService userChat)
        {
            _userChat= userChat;
        }
        [HttpGet(nameof(GetAppUserChats))]
        public IActionResult GetAppUserChats(string from, string to)
        {
            var obj = _userChat.GetUserChatHistory(from, to);
            if (obj == null)
            {
                return NotFound($"The Chat Data not found in the system.");
            }
            else
            {
                return Ok(obj);
            }
        }
    }
}
