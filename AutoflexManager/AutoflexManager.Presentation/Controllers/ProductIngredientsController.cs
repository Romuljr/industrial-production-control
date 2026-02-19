using Autoflex.Application.DTOs;
using Autoflex.Domain.Interfaces;
using AutoflexManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Autoflex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductIngredientsController : ControllerBase
    {
        private readonly IProductIngredientRepository _repository;

        public ProductIngredientsController(IProductIngredientRepository repository)
            => _repository = repository;

        // READ: Listar todas as associações
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ingredients = await _repository.GetAllAsync();

            var response = ingredients.Select(pi => new ProductIngredientResponseDTO
            {
                ProductId = pi.ProductId,
                ProductName = pi.Product?.Name ?? "N/A",
                RawMaterialId = pi.RawMaterialId,
                RawMaterialName = pi.RawMaterial?.Name ?? "N/A",
                RequiredQuantity = pi.RequiredQuantity
            });

            return Ok(response);
        }

        // READ: Buscar uma associação específica pela chave composta
        [HttpGet("{productId}/{rawMaterialId}")]
        public async Task<IActionResult> GetById(Guid productId, Guid rawMaterialId)
        {
            var ingredient = await _repository.GetAsync(productId, rawMaterialId);
            return ingredient == null ? NotFound() : Ok(ingredient);
        }

        // CREATE: Adicionar ingrediente (O seu método, mas com retorno CreatedAtAction)
        [HttpPost]
        public async Task<IActionResult> AddIngredient(ProductIngredientDTO dto)
        {
            var ingredient = new ProductIngredient
            {
                ProductId = dto.ProductId,
                RawMaterialId = dto.RawMaterialId,
                RequiredQuantity = dto.RequiredQuantity
            };
            await _repository.AddAsync(ingredient);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { productId = ingredient.ProductId, rawMaterialId = ingredient.RawMaterialId },
                ingredient);
        }

        // UPDATE: Atualizar a quantidade necessária
        [HttpPut]
        public async Task<IActionResult> Update(ProductIngredientDTO dto)
        {
            var entity = await _repository.GetAsync(dto.ProductId, dto.RawMaterialId);
            if (entity == null) return NotFound("Relação Produto x Matéria-Prima não encontrada.");

            entity.RequiredQuantity = dto.RequiredQuantity;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: Remover ingrediente da receita
        [HttpDelete("{productId}/{rawMaterialId}")]
        public async Task<IActionResult> Delete(Guid productId, Guid rawMaterialId)
        {
            var entity = await _repository.GetAsync(productId, rawMaterialId);
            if (entity == null) return NotFound();

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}