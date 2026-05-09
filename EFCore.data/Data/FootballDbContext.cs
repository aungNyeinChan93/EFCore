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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //TeacherUser
            modelBuilder.Entity<TeacherUser>()
                .HasKey(tu => new { tu.TeacherId, tu.UserId });

            modelBuilder.Entity<TeacherUser>()
                .HasOne(tu => tu.Teacher)
                .WithMany(t => t.TeacherUser)
                .HasForeignKey(tu => tu.TeacherId);

            modelBuilder.Entity<TeacherUser>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TeacherUser)
                .HasForeignKey(tu => tu.UserId);


            //CategoryPost
            modelBuilder.Entity<CategoryPost>()
                .HasKey(cp => new { cp.CategoryId, cp.PostId });

            modelBuilder.Entity<CategoryPost>()
                .HasOne(cp => cp.Category)
                .WithMany(c => c.CategoryPost)
                .HasForeignKey(cp => cp.CategoryId);

            modelBuilder.Entity<CategoryPost>()
                .HasOne(cp => cp.Post)
                .WithMany(p => p.CategoryPost)
                .HasForeignKey(cp => cp.PostId);

            //mtm match
            modelBuilder.Entity<Match>()
                .HasKey(m => new { m.HomeTeamId, m.AwayTeamId });

            modelBuilder.Entity<Match>()
                .HasOne(m=> m.HomeTeam)
                .WithMany(t=>t.HomeMatches)
                .HasForeignKey(m=>m.HomeTeamId);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId);

        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public  DbSet<School> Schools { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<TeacherUser> TeacherUsers { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryPost> CategoryPosts { get; set; }

        public DbSet<Match> Matches { get; set; }
    }
}
