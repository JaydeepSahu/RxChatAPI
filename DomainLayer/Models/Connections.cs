using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DomainLayer.EFModels
{
    public partial class Connections:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string SignalrId { get; set; }
    }
}
