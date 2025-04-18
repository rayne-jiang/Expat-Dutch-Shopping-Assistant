import React from 'react';
import { ConfigProvider } from 'antd';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import ProductSearch from './components/ProductSearch';
import './App.css';

const theme = createTheme({
  palette: {
    primary: {
      main: '#1890ff',
    },
  },
});

function App() {
  return (
    <ConfigProvider
      theme={{
        token: {
          colorPrimary: '#1890ff',
        },
      }}
    >
      <ThemeProvider theme={theme}>
        <ProductSearch />
      </ThemeProvider>
    </ConfigProvider>
  );
}

export default App;
