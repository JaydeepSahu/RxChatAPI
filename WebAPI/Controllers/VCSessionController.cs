using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTokSDK;

using DomainLayer.Models;
using DomainLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class VCSessionController : ControllerBase
    {
        private IConfiguration _Configuration;
        private ApplicationDbContext _context;
        public VCSessionController(IConfiguration config, ApplicationDbContext context)
        {
            _Configuration = config;
            _context = context; 
        }
        public class RoomForm
        {
            public string RoomName { get; set; }
        }

        [HttpPost("GetSession")]
        public IActionResult GetSession([FromBody] RoomForm roomForm)
        {
            var apiKey = int.Parse(_Configuration["ApiKey"]);
            var apiSecret = _Configuration["ApiSecret"];
            var opentok = new OpenTok(apiKey, apiSecret);
            var roomName = roomForm.RoomName;
            string sessionId;
            string token;
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
                var room = _context.VCRoom.Where(r => r.RoomName == roomName).FirstOrDefault();
                if (room != null)
                {
                    sessionId = room.SessionId;
                    token = opentok.GenerateToken(sessionId);
                    room.Token = token;
                    _context.SaveChanges();
                }
                else
                {
                    var session = opentok.CreateSession();
                    sessionId = session.Id;
                    token = opentok.GenerateToken(sessionId);
                    var roomInsert = new VCRoom
                    {
                        SessionId = sessionId,
                        Token = token,
                        RoomName = roomName
                    };
                    _context.Add(roomInsert);
                    _context.SaveChanges();
                }
            //}
            return Ok(new { sessionId = sessionId, token = token, apiKey = _Configuration["ApiKey"] });
        }
    }
}
