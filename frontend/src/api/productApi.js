import axios from 'axios';

const apiUrl = import.meta.env.VITE_API_URL;

export const fetchProducts = async () => {
    try {
        const response = await axios.get(`${apiUrl}/products`);
        return response.data;
    } catch (error) {
        console.error("Error fetching products:", error);
        throw error;
    }
};

export const fetchMostViewedProducts = async () => {
    try {
        const response = await axios.get(`${apiUrl}/products/most-viewed`);
        return response.data;
    } catch (error) {
        console.error("Error fetching products:", error);
        throw error;
    }
};

export const fetchProductsWithDiscounts = async () => {
    try {
        const response = await axios.get(`${apiUrl}/products/discounts`);
        return response.data;
    } catch (error) {
        console.error("Error fetching products:", error);
        throw error;
    }
};