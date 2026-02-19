import { useEffect, useState } from 'react';
import api from '../services/api';
import type { ProductIngredient } from '../types';

export default function IngredientList() {
  const [ingredients, setIngredients] = useState<ProductIngredient[]>([]);

  useEffect(() => {
    api.get('/ProductIngredients') // Ajuste para sua rota de receitas
      .then(res => setIngredients(res.data))
      .catch(err => console.error(err));
  }, []);

  return (
    <div className="bg-white shadow-md rounded-xl p-6 mt-8 border-t-4 border-purple-500">
      <h2 className="text-xl font-bold text-gray-800 mb-6 flex items-center gap-2">
        🧪 Ficha Técnica e Vínculos de Materiais
      </h2>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {ingredients.map((item, index) => (
          <div key={index} className="border border-purple-100 rounded-lg p-4 bg-purple-50/30">
            <p className="text-xs font-black text-purple-400 uppercase">Produto</p>
            <h3 className="text-gray-800 font-bold mb-3">{item.productName}</h3>
            <div className="flex justify-between items-center bg-white p-2 rounded border border-purple-100">
              <span className="text-sm text-gray-600">{item.rawMaterialName}</span>
              <span className="text-sm font-bold text-purple-700">Qtd: {item.requiredQuantity}</span>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}