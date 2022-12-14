using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class VCRoom:BaseEntity
    {
            [Key]
            public int Id { get; set; }
            public string SessionId { get; set; }
            public string RoomName { get; set; }
            public string Token { get; set; }
    }
}
