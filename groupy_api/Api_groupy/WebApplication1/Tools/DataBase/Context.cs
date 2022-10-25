using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Localidade;

namespace WebApplication1.Tools.DataBase
{
    public class Context : IdentityDbContext<User>
    {

        public Context(DbContextOptions<Context> options)
            : base(options)
        {}
        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ImageModel> Image { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Notification> Notification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasOne(c => c.State)
                .WithMany(c => c.Citys);

            modelBuilder.Entity<Group>()
                .HasOne(c => c.Leader);

            modelBuilder.Entity<Group>()
                .HasMany(c => c.Participants)
                .WithMany(e => e.Groups);
          
            modelBuilder.Entity<Group>()
                .HasOne(c => c.Address);

            modelBuilder
                .Entity<ImageModel>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasOne(c => c.Address);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Participants);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.SenderUser);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Group);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.ReciverUser);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(n => n.Group)
                .WithMany(g => g.Messages);

            base.OnModelCreating(modelBuilder);
        }
    }

}
