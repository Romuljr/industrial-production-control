import { useEffect, useState } from 'react';
import api from '../services/api';
import type { Product } from '../types';

export default function ProductList() {
  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    api.get('/Products').then(res => setProducts(res.data));
  }, []);

  return (
    <div className="bg-white shadow-md rounded-xl p-6">
      <h2 className="text-xl font-bold text-gray-800 mb-4 italic">📋 Catálogo de Produtos</h2>
      <ul className="divide-y divide-gray-100">
        {products.map(p => (
          <li key={p.id} className="py-3 flex justify-between items-center">
            <span className="font-medium text-gray-700">{p.name}</span>
            <span className="bg-blue-50 text-blue-700 px-3 py-1 rounded-lg text-sm font-bold">
              R$ {p.price.toFixed(2)}
            </span>
          </li>
        ))}
      </ul>
    </div>
  );
}