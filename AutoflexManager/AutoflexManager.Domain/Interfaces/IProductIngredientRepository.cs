using AutoflexManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Domain.Interfaces
{
    public interface IProductIngredientRepository
    {
        Task AddAsync(ProductIngredient ingredient);

        void Delete(ProductIngredient ingredient);

        Task<ProductIngredient?> GetAsync(Guid productId, Guid rawMaterialId);
        Task<IEnumerable<ProductIngredient>> GetAllAsync();
        void Update(ProductIngredient ingredient);
        Task SaveChangesAsync();
    }
}
