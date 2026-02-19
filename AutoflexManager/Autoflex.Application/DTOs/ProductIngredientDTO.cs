using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Application.DTOs
{
    public class ProductIngredientDTO
    {
        public Guid ProductId { get; set; }
        public Guid RawMaterialId { get; set; }
        public double RequiredQuantity { get; set; }
    }
}
