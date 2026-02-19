using Autoflex.Application.DTOs;
using Autoflex.Domain.Interfaces;
using AutoflexManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoflex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RawMaterialsController : ControllerBase
    {
        private readonly IRawMaterialRepository _repository;

        public RawMaterialsController(IRawMaterialRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var material = await _repository.GetByIdAsync(id);
            return material == null ? NotFound() : Ok(material);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RawMaterialDTO dto)
        {
            var material = new RawMaterial
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                StockQuantity = dto.StockQuantity
            };
            await _repository.AddAsync(material);
            await _repository.SaveChangesAsync();
            return Ok(material);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RawMaterialDTO dto)
        {
            var material = await _repository.GetByIdAsync(id);
            if (material == null) return NotFound();

            material.Name = dto.Name;
            material.StockQuantity = dto.StockQuantity;

            _repository.Update(material);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var material = await _repository.GetByIdAsync(id);
            if (material == null) return NotFound();

            _repository.Delete(material);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
