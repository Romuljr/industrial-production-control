import { useEffect, useState } from 'react';
import api from '../services/api';
import type { RawMaterial } from '../types';

export default function RawMaterialCRUD() {
  const [materials, setMaterials] = useState<RawMaterial[]>([]);
  const [formData, setFormData] = useState({ name: '', stockQuantity: 0 });
  const [editingId, setEditingId] = useState<string | null>(null);

  const loadData = () => api.get('/RawMaterials').then(res => setMaterials(res.data));
  useEffect(() => { loadData(); }, []);

  const handleSave = async (e: React.FormEvent) => {
    e.preventDefault();
    editingId ? await api.put(`/RawMaterials/${editingId}`, formData) : await api.post('/RawMaterials', formData);
    setFormData({ name: '', stockQuantity: 0 });
    setEditingId(null);
    loadData();
  };

  return (
    <div className="space-y-6">
      <h2 className="text-2xl font-bold text-slate-800">Controle de Matérias-Primas</h2>
      <form onSubmit={handleSave} className="bg-white p-6 rounded-xl shadow-sm border flex gap-4 items-end">
        <div className="flex-1">
          <label className="block text-xs font-bold text-slate-400 mb-1">INSUMO</label>
          <input className="w-full p-2 border rounded" value={formData.name} onChange={e => setFormData({...formData, name: e.target.value})} />
        </div>
        <div className="w-32">
          <label className="block text-xs font-bold text-slate-400 mb-1">ESTOQUE</label>
          <input type="number" className="w-full p-2 border rounded" value={formData.stockQuantity} onChange={e => setFormData({...formData, stockQuantity: Number(e.target.value)})} />
        </div>
        <button className="bg-orange-600 text-white px-6 py-2 rounded font-bold">SALVAR</button>
      </form>
      <div className="bg-white rounded-xl shadow-sm border overflow-hidden">
        <table className="w-full text-left text-sm">
          <thead className="bg-slate-50 border-b">
            <tr><th className="p-4">MATERIAL</th><th className="p-4">QTD</th><th className="p-4 text-right">AÇÕES</th></tr>
          </thead>
          <tbody>
            {materials.map(m => (
              <tr key={m.id} className="border-b">
                <td className="p-4 font-medium">{m.name}</td>
                <td className="p-4 font-bold">{m.stockQuantity} un.</td>
                <td className="p-4 text-right space-x-3">
                  <button className="text-blue-500 font-bold" onClick={() => { setEditingId(m.id); setFormData({name: m.name, stockQuantity: m.stockQuantity}) }}>Editar</button>
                  <button className="text-red-500 font-bold" onClick={async () => { if(confirm("Excluir?")) { await api.delete(`/RawMaterials/${m.id}`); loadData(); } }}>Excluir</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}