using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Application.DTOs
{
    public class RawMaterialDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double StockQuantity { get; set; }
    }
}
