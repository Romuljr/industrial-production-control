// Para a lista geral de Cadastro de Produtos
export interface Product {
  id: string;
  name: string;
  price: number;
}

// Para o Estoque de Matérias-Primas
export interface RawMaterial {
  id: string;
  name: string;
  stockQuantity: number;
}

// Para o vínculo de Receitas
export interface ProductIngredient {
  productId: string;      // Adicione esta linha
  productName: string;
  rawMaterialId: string;  // Adicione esta linha
  rawMaterialName: string;
  requiredQuantity: number;
}

// Interface EXCLUSIVA para a regra de negócio (O cálculo do Back-end)
export interface ProductionSuggestion {
  productName: string;
  quantityToProduce: number;
  totalValue: number;
}