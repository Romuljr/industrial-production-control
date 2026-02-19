using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Application.DTOs
{
    public class ProductIngredientResponseDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid RawMaterialId { get; set; }
        public string RawMaterialName { get; set; }
        public double RequiredQuantity { get; set; }
    }
}
