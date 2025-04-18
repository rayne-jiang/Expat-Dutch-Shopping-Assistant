import React from 'react';
import { Input, Button } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
import { ProductSearchProps } from '../types/product';
import '../styles/SearchBar.css';

const { Search } = Input;

const SearchBar: React.FC<ProductSearchProps> = ({ onSearch, loading }) => {
    return (
        <div className="search-form">
            <Search
                placeholder="Enter product name..."
                allowClear
                enterButton={<Button type="primary" icon={<SearchOutlined />}>Search</Button>}
                size="large"
                onSearch={onSearch}
                loading={loading}
            />
        </div>
    );
};

export default SearchBar; 