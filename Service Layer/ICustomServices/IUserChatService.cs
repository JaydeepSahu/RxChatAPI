using DomainLayer.Models;
using Repository_Layer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.ICustomServices
{
    public interface IUserChatService
    {
        IEnumerable<UserChat> GetUserChatHistory(string from, string to);
        IEnumerable<UserChat> GetAll();
        UserChat Get(int Id);
        void Insert(UserChat entity);
        void Update(UserChat entity);
        void Delete(UserChat entity);
    }
}
