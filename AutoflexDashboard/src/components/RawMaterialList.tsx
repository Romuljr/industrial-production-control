import { useEffect, useState } from 'react';
import api from '../services/api';
import type { RawMaterial } from '../types';

export default function RawMaterialList() {
  const [materials, setMaterials] = useState<RawMaterial[]>([]);

  useEffect(() => {
    api.get('/RawMaterials').then(res => setMaterials(res.data));
  }, []);

  return (
    <div className="bg-white shadow-md rounded-xl p-6 border-t-4 border-orange-500">
      <h2 className="text-xl font-bold text-gray-800 mb-4 italic">📦 Estoque de Insumos</h2>
      <div className="space-y-4">
        {materials.map(m => (
          <div key={m.id} className="flex justify-between items-center">
            <span className="text-gray-600">{m.name}</span>
            <span className={`font-mono font-bold ${m.stockQuantity < 10 ? 'text-red-500' : 'text-gray-800'}`}>
              {m.stockQuantity} un.
            </span>
          </div>
        ))}
      </div>
    </div>
  );
}