import { useEffect, useState } from 'react';
import api from '../services/api';
import type { Product } from '../types';

export default function ProductCRUD() {
  const [products, setProducts] = useState<Product[]>([]);
  const [formData, setFormData] = useState({ name: '', price: 0 });
  const [editingId, setEditingId] = useState<string | null>(null);

  const loadData = () => api.get('/Products').then(res => setProducts(res.data));

  useEffect(() => { loadData(); }, []);

  const handleSave = async (e: React.FormEvent) => {
    e.preventDefault();
    if (editingId) {
      await api.put(`/Products/${editingId}`, formData);
    } else {
      await api.post('/Products', formData);
    }
    setFormData({ name: '', price: 0 });
    setEditingId(null);
    loadData();
  };

  const handleDelete = async (id: string) => {
    if (confirm("Excluir este produto?")) {
      await api.delete(`/Products/${id}`);
      loadData();
    }
  };

  return (
    <div className="space-y-6">
      <h2 className="text-2xl font-bold text-slate-800">Gerenciar Produtos</h2>
      
      <form onSubmit={handleSave} className="bg-white p-6 rounded-xl shadow-sm border flex gap-4 items-end">
        <div className="flex-1">
          <label className="block text-xs font-bold text-slate-400 mb-1">NOME</label>
          <input className="w-full p-2 border rounded" value={formData.name} onChange={e => setFormData({...formData, name: e.target.value})} />
        </div>
        <div className="w-32">
          <label className="block text-xs font-bold text-slate-400 mb-1">PREÇO</label>
          <input type="number" className="w-full p-2 border rounded" value={formData.price} onChange={e => setFormData({...formData, price: Number(e.target.value)})} />
        </div>
        <button className="bg-blue-600 text-white px-6 py-2 rounded font-bold">{editingId ? 'SALVAR' : 'CADASTRAR'}</button>
      </form>

      <div className="bg-white rounded-xl shadow-sm border overflow-hidden">
        <table className="w-full text-left">
          <thead className="bg-slate-50 border-b text-xs font-bold text-slate-400">
            <tr><th className="p-4">NOME</th><th className="p-4">PREÇO</th><th className="p-4 text-right">AÇÕES</th></tr>
          </thead>
          <tbody>
            {products.map(p => (
              <tr key={p.id} className="border-b hover:bg-slate-50">
                <td className="p-4 font-bold">{p.name}</td>
                <td className="p-4 text-blue-600">R$ {p.price.toFixed(2)}</td>
                <td className="p-4 text-right space-x-3">
                  <button className="text-blue-500 text-sm font-bold" onClick={() => { setEditingId(p.id); setFormData({name: p.name, price: p.price}) }}>Editar</button>
                  <button className="text-red-500 text-sm font-bold" onClick={() => handleDelete(p.id)}>Excluir</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}