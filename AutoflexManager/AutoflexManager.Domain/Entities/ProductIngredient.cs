using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoflexManager.Domain.Entities
{
    public class ProductIngredient
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid RawMaterialId { get; set; }
        public RawMaterial RawMaterial { get; set; }

        public double RequiredQuantity { get; set; }
    }
}
