import Header from "../components/Header"
import ProductCard from "../components/ProductCard"
import "./Home.css"

export default function Home() {

    return (
        <>
            <Header></Header>
            <div className="content-container">
                <div className="content">
                    <div className="product-section">
                        <h3>Most popular products</h3>
                        <div className="product-panel">
                            <ProductCard></ProductCard>
                            <ProductCard></ProductCard>
                            <ProductCard></ProductCard>
                            <ProductCard></ProductCard>
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