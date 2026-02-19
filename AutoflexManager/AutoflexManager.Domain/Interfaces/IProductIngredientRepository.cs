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
        // Para adicionar um insumo a um produto
        Task AddAsync(ProductIngredient ingredient);

        // Para remover um insumo de um produto
        void Delete(ProductIngredient ingredient);

        // Para buscar uma associação específica e validar se já existe
        Task<ProductIngredient?> GetAsync(Guid productId, Guid rawMaterialId);
        Task<IEnumerable<ProductIngredient>> GetAllAsync();
        void Update(ProductIngredient ingredient);
        Task SaveChangesAsync();
    }
}
