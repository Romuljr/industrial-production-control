import { useEffect, useState } from 'react';
import api from '../services/api';
import type { Product, RawMaterial, ProductIngredient } from '../types';

export default function IngredientCRUD() {
  const [products, setProducts] = useState<Product[]>([]);
  const [materials, setMaterials] = useState<RawMaterial[]>([]);
  const [ingredients, setIngredients] = useState<ProductIngredient[]>([]);
  
  const [formData, setFormData] = useState({ 
    productId: '', 
    rawMaterialId: '', 
    requiredQuantity: 0 
  });

  const loadData = async () => {
    const [resP, resM, resI] = await Promise.all([
      api.get('/Products'),
      api.get('/RawMaterials'),
      api.get('/ProductIngredients')
    ]);
    setProducts(resP.data);
    setMaterials(resM.data);
    setIngredients(resI.data);
  };

  useEffect(() => { loadData(); }, []);

  const handleSave = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.post('/ProductIngredients', formData);
      setFormData({ productId: '', rawMaterialId: '', requiredQuantity: 0 });
      loadData();
      alert("Vínculo salvo com sucesso!");
    } catch (err) {
      console.error(err);
      alert("Erro ao salvar. Verifique se os campos foram selecionados corretamente.");
    }
  };

  return (
    <div className="space-y-8">
      <h2 className="text-2xl font-bold text-slate-800 italic">🧪 Fichas Técnicas (Engenharia)</h2>

      <form onSubmit={handleSave} className="bg-white p-6 rounded-xl shadow-lg border-2 border-blue-100 space-y-4">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {/* Select de Produto usando ID */}
          <div>
            <label className="block text-xs font-black text-blue-400 mb-1 uppercase">Produto Final</label>
            <select 
              className="w-full p-3 border rounded-lg bg-slate-50 outline-none focus:ring-2 focus:ring-blue-500"
              value={formData.productId}
              onChange={e => setFormData({...formData, productId: e.target.value})}
              required
            >
              <option value="">Selecione um produto...</option>
              {products.map(p => <option key={p.id} value={p.id}>{p.name}</option>)}
            </select>
          </div>

          {/* Select de Material usando ID */}
          <div>
            <label className="block text-xs font-black text-blue-400 mb-1 uppercase">Matéria-Prima</label>
            <select 
              className="w-full p-3 border rounded-lg bg-slate-50 outline-none focus:ring-2 focus:ring-blue-500"
              value={formData.rawMaterialId}
              onChange={e => setFormData({...formData, rawMaterialId: e.target.value})}
              required
            >
              <option value="">Selecione um insumo...</option>
              {materials.map(m => <option key={m.id} value={m.id}>{m.name}</option>)}
            </select>
          </div>
        </div>

        <div className="flex gap-4 items-end border-t pt-4">
          <div className="flex-1">
            <label className="block text-xs font-black text-slate-400 mb-1 uppercase">Quantidade Necessária</label>
            <input 
              type="number" step="0.01" 
              className="w-full p-3 border rounded-lg bg-slate-50" 
              value={formData.requiredQuantity}
              onChange={e => setFormData({...formData, requiredQuantity: Number(e.target.value)})} 
              required
            />
          </div>
          <button type="submit" className="bg-blue-600 text-white px-10 py-3 rounded-lg font-black hover:bg-blue-700 transition-all shadow-lg">
            VINCULAR
          </button>
        </div>
      </form>

      {/* Tabela de Vínculos com o Delete por IDs */}
      <div className="bg-white rounded-xl shadow-sm border border-slate-200 overflow-hidden">
        <table className="w-full text-left">
          <thead className="bg-slate-50 border-b text-[10px] font-black text-slate-400 uppercase tracking-widest">
            <tr>
              <th className="p-4">Produto</th>
              <th className="p-4">Material</th>
              <th className="p-4">Qtd</th>
              <th className="p-4 text-right">Ação</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-100">
            {ingredients.map((ing, i) => (
              <tr key={i} className="hover:bg-blue-50/50">
                <td className="p-4 font-bold text-slate-700">{ing.productName}</td>
                <td className="p-4 text-slate-600">{ing.rawMaterialName}</td>
                <td className="p-4 font-mono text-blue-600 font-bold">{ing.requiredQuantity}</td>
                <td className="p-4 text-right">
                  <button 
                    onClick={async () => {
                      if(confirm("Remover este vínculo?")) {
                        await api.delete(`/ProductIngredients/${ing.productId}/${ing.rawMaterialId}`);
                        loadData();
                      }
                    }}
                    className="text-red-400 hover:text-red-600 font-bold text-xs"
                  >
                    EXCLUIR
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}