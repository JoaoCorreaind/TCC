using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Localidade;

namespace WebApplication1.Tools.DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {}
        public DbSet<User> User { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ImageModel> Image { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cidade>()
                .HasOne(c => c.Estado)
                .WithMany(c => c.Cidades);

            modelBuilder.Entity<Grupo>()
                .HasOne(c => c.Lider);

            modelBuilder.Entity<Grupo>()
                .HasMany(c => c.Participantes)
                .WithMany(e => e.Grupos);

            modelBuilder.Entity<Grupo>()
                .HasOne(c => c.Cidade)
                .WithMany(c=> c.Grupos);

            modelBuilder
                .Entity<ImageModel>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasOne(c => c.Cidade)
                .WithMany(c => c.Users);
        }
    }

}
