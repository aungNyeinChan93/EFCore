using EFCore.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.data.Data
{
    public class FootballDbContext :DbContext
    {

        private readonly string _connection = "Data Source=.;Initial Catalog=football_01;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connection);
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
