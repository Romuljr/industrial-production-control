using AutoflexManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura a chave primária composta da tabela associativa
            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.RawMaterialId });

            // Configura o relacionamento Produto -> Ingredientes
            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Ingredients)
                .HasForeignKey(pi => pi.ProductId);

            // Configura o relacionamento Matéria-Prima -> Ingredientes
            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.RawMaterial)
                .WithMany(rm => rm.ProductIngredients)
                .HasForeignKey(pi => pi.RawMaterialId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
