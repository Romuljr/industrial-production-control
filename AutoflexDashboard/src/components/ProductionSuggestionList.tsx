import { useEffect, useState } from 'react';
import api from '../services/api';
import type { ProductionSuggestion } from '../types';

export default function ProductionSuggestionList() {
  const [suggestions, setSuggestions] = useState<ProductionSuggestion[]>([]);

  useEffect(() => {
    api.get('/Production/suggestions')
      .then(res => setSuggestions(res.data))
      .catch(err => console.error("Erro ao carregar sugestões:", err));
  }, []);

  return (
    <section className="mb-10">
      <h2 className="text-2xl font-bold text-gray-800 mb-6 flex items-center gap-2 tracking-tight">
        📊 Planejamento de Demanda e Sugestão de Produção
      </h2>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {suggestions.map((s, index) => (
          <div key={index} className="bg-white border-l-8 border-green-500 shadow-lg rounded-xl p-5 hover:scale-105 transition-transform">
            <h3 className="text-sm font-bold text-gray-500 uppercase tracking-widest">{s.productName}</h3>
            <div className="mt-4 flex justify-between items-baseline">
              <span className="text-4xl font-black text-green-600">{s.quantityToProduce}</span>
              <span className="text-gray-400 font-bold uppercase text-xs">Unidades</span>
            </div>
            <p className="mt-2 text-sm text-gray-400 border-t pt-2">
              Valor Estimado: <span className="font-semibold text-gray-700">R$ {s.totalValue.toFixed(2)}</span>
            </p>
          </div>
        ))}
      </div>
    </section>
  );
}