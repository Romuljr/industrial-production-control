using Autoflex.Domain.Interfaces;
using Autoflex.Infrastructure.Data;
using AutoflexManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.Include(p => p.Ingredients).ThenInclude(i => i.RawMaterial).ToListAsync();

        public async Task<Product?> GetByIdAsync(Guid id) =>
            await _context.Products.Include(p => p.Ingredients).FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);

        public void Update(Product product) => _context.Products.Update(product);

        public void Delete(Product product) => _context.Products.Remove(product);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
