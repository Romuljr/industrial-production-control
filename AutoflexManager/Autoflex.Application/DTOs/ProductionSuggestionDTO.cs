using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Application.DTOs
{
    public class ProductionSuggestionDTO
    {
        public string ProductName { get; set; } = string.Empty;
        public int QuantityToProduce { get; set; }

        public decimal TotalValue { get; set; }
    }
}
