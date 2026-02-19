using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Application.DTOs
{
    public class ProductionSuggestionDTO
    {
        // Nome do produto para exibir na lista
        public string ProductName { get; set; } = string.Empty;

        // O resultado do cálculo de quantas unidades conseguimos fabricar
        public int QuantityToProduce { get; set; }

        // Preço Unitário * Quantidade (para mostrar o lucro potencial)
        public decimal TotalValue { get; set; }
    }
}
