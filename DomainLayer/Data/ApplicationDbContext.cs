using DomainLayer.EFModels;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        //public DbSet<Student> Students
        //{
        //    get;
        //    set;
        //}
        //public DbSet<Departments> Departments
        //{
        //    get;
        //    set;
        //}

        public DbSet<Connections> Connections
        {
            get;
            set;
        }

        public DbSet<Person> Person
        {
            get;
            set;
        }

        public DbSet<UserChat> UserChat
        {
            get;
            set;
        }

        public DbSet<ErrorResult> ErrorLog
        {
            get;
            set;
        }

        public DbSet<VCRoom> VCRoom
        {
            get;
            set;
        }
    }
}
