
export interface Product {
  id: string;
  name: string;
  price: number;
}


export interface RawMaterial {
  id: string;
  name: string;
  stockQuantity: number;
}


export interface ProductIngredient {
  productId: string;      
  productName: string;
  rawMaterialId: string;  
  rawMaterialName: string;
  requiredQuantity: number;
}

export interface ProductionSuggestion {
  productName: string;
  quantityToProduce: number;
  totalValue: number;
}