import Header from "../components/Header"
import ProductCarousel from "../components/ProductCarousel"
import { fetchMostViewedProducts, fetchProductsWithDiscounts } from "../api/productApi"
import { useState, useEffect } from "react"

export default function Home() {

    const [mostViewedProducts, setMostViewedProducts] = useState([])
    const [productsWithDiscounts, setProductsWithDiscounts] = useState([])

    useEffect(() => {
        const loadProducts = async () => {
            var data = await fetchMostViewedProducts()
            setMostViewedProducts(data)
            data = await fetchProductsWithDiscounts()
            setProductsWithDiscounts(data)
        };
        loadProducts();
    }, []);


    return (
        <>
            <Header></Header>
            <div className="page">
                <div className="content">
                    <ProductCarousel sectionTitle={'Best sellers'} products={mostViewedProducts}></ProductCarousel>
                    <ProductCarousel sectionTitle={'Discounts'} products={productsWithDiscounts}></ProductCarousel>
                </div>
            </div>

        </>
    )
}