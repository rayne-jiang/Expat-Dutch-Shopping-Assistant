import axios from 'axios';
import { Product } from '../types/product';

const API_BASE_URL = import.meta.env.VITE_API_URL;

interface ApiProduct {
    productName: string;
    imageUrl: string;
}

interface ApiResponse {
    productImages: ApiProduct[];
}

// Create axios instance with default config
const api = axios.create({
    baseURL: API_BASE_URL,
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json',
    }
});

export const productService = {
    searchProducts: async (searchTerm: string): Promise<Product[]> => {
        try {
            const response = await api.get<ApiResponse>('/JumboProduct/search', {
                params: { searchTerm }
            });
            // The API returns { productImages: Product[] }, so we need to access that property
            return response.data.productImages.map((product: ApiProduct) => ({
                name: product.productName,
                imageUrl: product.imageUrl
            }));
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.error('API Error:', {
                    message: error.message,
                    status: error.response?.status,
                    data: error.response?.data
                });
            } else {
                console.error('Unexpected error:', error);
            }
            throw error;
        }
    }
}; 