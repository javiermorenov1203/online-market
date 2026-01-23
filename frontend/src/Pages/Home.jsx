import Header from "../components/Header"
import HomeCarousel from "../components/HomeCarousel"
import { fetchMostViewedProducts, fetchProductsWithDiscounts } from "../api/productApi"
import { useState, useEffect } from "react"
import "./Home.css"

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
            <div className="content-container">
                <div className="content">
                    <div className="product-section">
                        <h3>Most popular products</h3>
                        <HomeCarousel products={mostViewedProducts}></HomeCarousel>
                    </div>
                    <div className="product-section">
                        <h3>Discounts</h3>
                        <HomeCarousel products={productsWithDiscounts}></HomeCarousel>
                    </div>
                </div>
            </div>

        </>
    )
}