export interface Product {
    name: string;
    imageUrl: string;
}

export interface ProductSearchProps {
    onSearch: (searchTerm: string) => void;
    loading: boolean;
}

export interface ProductCardProps {
    product: Product;
} 