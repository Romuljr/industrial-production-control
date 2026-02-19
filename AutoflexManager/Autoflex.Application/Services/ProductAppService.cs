using Autoflex.Application.DTOs;
using Autoflex.Application.Interfaces;
using Autoflex.Domain.Interfaces;
using AutoflexManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _repository;

        public ProductAppService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
        }

        public async Task AddAsync(ProductDTO dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price
            };
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
        }
    }
}
