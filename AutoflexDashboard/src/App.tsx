import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';

// Importação dos Componentes da Home (Dashboard)
import ProductionSuggestionList from './components/ProductionSuggestionList';
import ProductList from './components/ProductList';
import RawMaterialList from './components/RawMaterialList';
import IngredientList from './components/IngredientList';

// Importação das Páginas de Gestão
import ProductCRUD from './pages/ProductCRUD';
import RawMaterialCRUD from './pages/RawMaterialCRUD';
import IngredientCRUD from './pages/IngredientCRUD';

// Componente Interno para a Página Inicial (Dashboard)
function Home() {
  return (
    <div className="space-y-8">
      <header className="mb-8">
        <h2 className="text-3xl font-black text-slate-800 tracking-tight">Painel Geral</h2>
        <p className="text-slate-500 font-medium">Visão consolidada da operação e sugestões de demanda.</p>
      </header>
      
      {/* Sugestão de Produção - Destaque no Topo */}
      <ProductionSuggestionList />

      {/* Listas de Apoio - Duas Colunas */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
        <ProductList />
        <RawMaterialList />
      </div>

      {/* Vínculos Técnicos abaixo */}
      <IngredientList />
    </div>
  );
}

export default function App() {
  return (
    <Router>
      <div className="min-h-screen bg-slate-50 flex flex-col md:flex-row">
        
        {/* Menu Lateral Estilizado */}
        <nav className="w-full md:w-64 bg-slate-900 text-white p-6 flex flex-col gap-2 min-h-screen shadow-2xl">
          <div className="mb-10 px-3">
            <h1 className="text-2xl font-black text-blue-500 uppercase tracking-tighter leading-none">
              Autoflex <span className="text-white block text-sm font-light tracking-widest mt-1">Ops Manager</span>
            </h1>
          </div>
          
          <Link 
            to="/" 
            className="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-slate-800 transition-all font-semibold text-slate-300 hover:text-white group"
          >
            <span className="group-hover:scale-110 transition-transform">🏠</span> Painel Geral
          </Link>
          
          <div className="mt-8 mb-2 px-4 text-[10px] font-black text-slate-500 uppercase tracking-[0.2em]">
            Configurações
          </div>
          
          <Link 
            to="/produtos" 
            className="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-slate-800 transition-all font-semibold text-slate-300 hover:text-white group"
          >
            <span className="group-hover:scale-110 transition-transform">📦</span> Produtos
          </Link>
          
          <Link 
            to="/materiais" 
            className="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-slate-800 transition-all font-semibold text-slate-300 hover:text-white group"
          >
            <span className="group-hover:scale-110 transition-transform">🧱</span> Matérias-Primas
          </Link>
          
          <Link 
            to="/vinculos" 
            className="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-slate-800 transition-all font-semibold text-slate-300 hover:text-white group"
          >
            <span className="group-hover:scale-110 transition-transform">🧪</span> Vínculos/Receitas
          </Link>

          {/* Rodapé do Menu */}
          <div className="mt-auto pt-10 px-4">
            <div className="bg-slate-800/50 p-3 rounded-lg border border-slate-700/50">
              <p className="text-[10px] text-slate-500 font-bold uppercase">Status</p>
              <div className="flex items-center gap-2 mt-1">
                <div className="w-2 h-2 bg-green-500 rounded-full animate-pulse"></div>
                <span className="text-xs font-bold text-slate-300">Servidor Online</span>
              </div>
            </div>
          </div>
        </nav>

        {/* Área de Conteúdo Fluida */}
        <main className="flex-1 p-6 md:p-10 overflow-y-auto max-h-screen">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/produtos" element={<ProductCRUD />} />
            <Route path="/materiais" element={<RawMaterialCRUD />} />
            <Route path="/vinculos" element={<IngredientCRUD />} />
          </Routes>
        </main>

      </div>
    </Router>
  );
}