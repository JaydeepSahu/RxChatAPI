using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class BaseEntity
    {
        public int Id
        {
            get;
            set;
        }
        public DateTime AddedOn
        {
            get;
            set;
        }
        public DateTime UpdatedOn
        {
            get;
            set;
        }
        public bool IsActive
        {
            get;
            set;
        }
    }
}
