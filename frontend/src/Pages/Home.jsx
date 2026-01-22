import Header from "../components/Header"
import ProductCard from "../components/ProductCard"
import { fetchProducts } from "../api/productApi"
import { useState, useEffect } from "react"
import "./Home.css"

export default function Home() {

    const [products, setProducts] = useState([])

    useEffect(() => {
        const loadProducts = async () => {
            const data = await fetchProducts()
            setProducts(data)
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
                        <div className="product-panel">
                            {products.map(p => (
                                <ProductCard key={p.id} product={p} />
                            ))}
                        </div>
                    </div>
                    <div className="product-section">
                        <h3>Discounts</h3>
                        <div className="product-panel">
                            <div className="product-card"></div>
                            <div className="product-card"></div>
                            <div className="product-card"></div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}