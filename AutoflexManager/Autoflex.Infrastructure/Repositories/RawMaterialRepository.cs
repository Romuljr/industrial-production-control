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
    public class RawMaterialRepository : IRawMaterialRepository
    {
        private readonly AppDbContext _context;
        public RawMaterialRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<RawMaterial>> GetAllAsync() =>
            await _context.RawMaterials.ToListAsync();

        public async Task<RawMaterial?> GetByIdAsync(Guid id) =>
            await _context.RawMaterials.FindAsync(id);

        public async Task AddAsync(RawMaterial rawMaterial) =>
            await _context.RawMaterials.AddAsync(rawMaterial);

        public void Update(RawMaterial rawMaterial) => _context.RawMaterials.Update(rawMaterial);

        public void Delete(RawMaterial rawMaterial) => _context.RawMaterials.Remove(rawMaterial);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
