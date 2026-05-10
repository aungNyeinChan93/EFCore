using EFCore.Data_03.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Data_03.Data
{
    public class PeopleDbContext : DbContext
    {

        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Email> EmailAddresses { get; set; }
        public DbSet<Person> People { get; set; }

        public DbSet<Todo> Todos { get; set; }
    }
}
