using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class UserChat:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}
