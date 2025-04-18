import React from 'react';
import { Card } from 'antd';
import { ProductCardProps } from '../types/product';
import '../styles/ProductCard.css';

const ProductCard: React.FC<ProductCardProps> = ({ product }) => {
    return (
        <Card
            hoverable
            className="product-card"
            cover={
                <img
                    alt={product.name}
                    src={product.imageUrl}
                    className="product-image"
                />
            }
        >
            <Card.Meta
                title={product.name}
                className="product-name"
            />
        </Card>
    );
};

export default ProductCard; 