using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.IRepository;
using Service_Layer.ICustomServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.CustomServices
{
    public class UserChatService: IUserChatService
    {
        private readonly IRepository<UserChat> _userChat;
        public ApplicationDbContext _context;
        public UserChatService(IRepository<UserChat> userChat, ApplicationDbContext context)
        {
            _userChat = userChat;
            _context = context;
        }

        public void Delete(UserChat entity)
        {
            _userChat.Delete(entity);
        }

        public UserChat Get(int Id)
        {
            return _userChat.Get(Id);
        }

        public IEnumerable<UserChat> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserChat> GetUserChatHistory(string from, string to)
        {
            return _context.UserChat.Where(x => (x.From == from || x.From == to) && (x.To == from || x.To == to));
            //return _userChat.GetAll();
        }

        public void Insert(UserChat entity)
        {
            throw new NotImplementedException();
        }

        public void Update(UserChat entity)
        {
            throw new NotImplementedException();
        }
    }
}
