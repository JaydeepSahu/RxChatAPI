using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Departments : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string DepartmentName
        {
            get;
            set;
        }
        public string DepartmentDescription
        {
            get;
            set;
        }
        public int StudentId
        {
            get;
            set;
        }
        public Student Students
        {
            get;
            set;
        }
    }
}
