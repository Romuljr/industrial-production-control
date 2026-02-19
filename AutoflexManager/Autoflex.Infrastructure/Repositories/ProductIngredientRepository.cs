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
    public class ProductIngredientRepository : IProductIngredientRepository
    {
        private readonly AppDbContext _context;
        public ProductIngredientRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(ProductIngredient ingredient) =>
            await _context.ProductIngredients.AddAsync(ingredient);

        public void Delete(ProductIngredient ingredient) =>
            _context.ProductIngredients.Remove(ingredient);

        public async Task<IEnumerable<ProductIngredient>> GetAllAsync()
        {
            return await _context.ProductIngredients
                .Include(pi => pi.Product)      // Traz os dados do Produto
                .Include(pi => pi.RawMaterial)  // Traz os dados da Matéria-Prima
                .ToListAsync();
        }
        public void Update(ProductIngredient ingredient) =>
        _context.Entry(ingredient).State = EntityState.Modified;

        public async Task<ProductIngredient?> GetAsync(Guid productId, Guid rawMaterialId) =>
            await _context.ProductIngredients
                .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.RawMaterialId == rawMaterialId);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
