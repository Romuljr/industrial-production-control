using Autoflex.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Application.Interfaces
{
    public interface IProductAppService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task AddAsync(ProductDTO dto);
    }
}
