using Autoflex.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoflex.Application.Services
{
    public interface IProductionService
    {
        Task<IEnumerable<ProductionSuggestionDTO>> GetProductionSuggestionsAsync();
    }
}
