using Autoflex.Application.DTOs;
using Autoflex.Application.Interfaces;
using Autoflex.Domain.Interfaces;

namespace Autoflex.Application.Services
{
    public class ProductionService : IProductionService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRawMaterialRepository _rawMaterialRepository;

        public ProductionService(IProductRepository productRepository, IRawMaterialRepository rawMaterialRepository)
        {
            _productRepository = productRepository;
            _rawMaterialRepository = rawMaterialRepository;
        }

        public async Task<IEnumerable<ProductionSuggestionDTO>> GetProductionSuggestionsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var rawMaterials = await _rawMaterialRepository.GetAllAsync();

            var virtualStock = rawMaterials.ToDictionary(rm => rm.Id, rm => rm.StockQuantity);
            var suggestions = new List<ProductionSuggestionDTO>();

            var sortedProducts = products.OrderByDescending(p => p.Price);

            foreach (var product in sortedProducts)
            {
                if (product.Ingredients == null || !product.Ingredients.Any())
                    continue;

                int maxPossibleForThisProduct = int.MaxValue;

                foreach (var ingredient in product.Ingredients)
                {
                    if (virtualStock.ContainsKey(ingredient.RawMaterialId))
                    {
                        var available = virtualStock[ingredient.RawMaterialId];
                        int canMakeWithThis = (int)(available / ingredient.RequiredQuantity);

                        if (canMakeWithThis < maxPossibleForThisProduct)
                            maxPossibleForThisProduct = canMakeWithThis;
                    }
                    else
                    {
                        maxPossibleForThisProduct = 0;
                    }
                }

                if (maxPossibleForThisProduct > 0)
                {
                    foreach (var ingredient in product.Ingredients)
                    {
                        virtualStock[ingredient.RawMaterialId] -= (maxPossibleForThisProduct * ingredient.RequiredQuantity);
                    }

                    suggestions.Add(new ProductionSuggestionDTO
                    {
                        ProductName = product.Name,
                        QuantityToProduce = maxPossibleForThisProduct,
                        TotalValue = (decimal)maxPossibleForThisProduct * product.Price
                    });
                }
            }

            return suggestions;
        }
    }
}
