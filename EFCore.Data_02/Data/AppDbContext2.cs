using EFCore.Data_02.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Data_02.Data
{
    public class AppDbContext2 :DbContext
    {

        private readonly string _connectionStr = "Data Source=.;Initial Catalog=entity_01;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;";
        
        public AppDbContext2()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //CategoryPost 
            modelBuilder.Entity<CategoryPost>()
                .HasKey(cp => new { cp.CategoryId, cp.PostId });

            modelBuilder.Entity<CategoryPost>()
                .HasOne(cp => cp.Category)
                .WithMany(c => c.CategoryPosts)
                .HasForeignKey(cp => cp.CategoryId);

            modelBuilder.Entity<CategoryPost>()
                .HasOne(cp => cp.Post)
                .WithMany(p => p.CategoryPosts)
                .HasForeignKey(cp => cp.PostId);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this._connectionStr);
            }
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AuthorDetail> AuthorDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryPost> CategoryPosts { get; set; }

    }
}
