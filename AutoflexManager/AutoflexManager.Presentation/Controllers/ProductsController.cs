using Autoflex.Application.DTOs;
using Autoflex.Domain.Interfaces;
using AutoflexManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoflex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();

            // Mapeando manualmente (ou use AutoMapper se souber)
            var dto = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // 1. Busca do repositório
            var product = await _repository.GetByIdAsync(id);

            // 2. Verifica se existe
            if (product == null) return NotFound();

            // 3. MAPEAMENTO: Transforma Entity em DTO para evitar o ciclo
            var dto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            // 4. Retorna o DTO (que não tem listas circulares)
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price
            };
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ProductDTO dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            product.Name = dto.Name;
            product.Price = dto.Price;

            _repository.Update(product);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            _repository.Delete(product);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
