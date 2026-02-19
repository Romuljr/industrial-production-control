using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoflexManager.Domain.Entities
{
    public class RawMaterial
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double StockQuantity { get; set; }

        public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();
    }
}
