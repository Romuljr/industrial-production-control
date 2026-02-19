using AutoflexManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Domain.Interfaces
{
    public interface IRawMaterialRepository
    {
        Task<IEnumerable<RawMaterial>> GetAllAsync();
        Task<RawMaterial?> GetByIdAsync(Guid id);
        Task AddAsync(RawMaterial rawMaterial);
        void Update(RawMaterial rawMaterial);
        void Delete(RawMaterial rawMaterial);
        Task SaveChangesAsync();
    }
}
