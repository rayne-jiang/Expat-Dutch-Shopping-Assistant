import React, { useState } from 'react';
import { Typography, Alert, Spin } from 'antd';
import { Box, Grid } from '@mui/material';
import { Product } from '../types/product';
import { productService } from '../services/productService';
import ProductCard from './ProductCard';
import SearchBar from './SearchBar';
import '../styles/ProductSearch.css';

const { Title } = Typography;

const ProductSearch: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const handleSearch = async (searchTerm: string) => {
        setLoading(true);
        setError('');

        try {
            const results = await productService.searchProducts(searchTerm);
            setProducts(results);
        } catch (error) {
            setError('Failed to fetch products. Please try again.' + error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <Box className="product-search-container">
            <Box className="search-header">
                <Title level={2}>Jumbo Product Search</Title>
            </Box>

            <SearchBar onSearch={handleSearch} loading={loading} />

            {error && (
                <Alert
                    message="Error"
                    description={error}
                    type="error"
                    showIcon
                    className="error-message"
                />
            )}

            {loading ? (
                <Box display="flex" justifyContent="center" mt={4}>
                    <Spin size="large" />
                </Box>
            ) : (
                <Grid container spacing={1} className="products-grid">
                    {products.map((product, index) => (
                        <Grid item xs={12} sm={6} md={3} lg={3} xl={3} key={index} sx={{ display: 'flex' }}>
                            <ProductCard product={product} />
                        </Grid>
                    ))}
                </Grid>
            )}
        </Box>
    );
};

export default ProductSearch; 