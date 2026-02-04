import Header from "../components/Header"
import ProductCarousel from "../components/ProductCarousel"
import Footer from "../components/Footer"
import { fetchMostViewedProducts, fetchProductsWithDiscounts } from "../api/productApi"
import { useState, useEffect, useRef } from "react"
import "./Home.css"

export default function Home() {

    const [mostViewedProducts, setMostViewedProducts] = useState([])
    const [productsWithDiscounts, setProductsWithDiscounts] = useState([])

    const homeImageRef = useRef(null);

    const scrollLeft = () => {
        homeImageRef.current.scrollBy({ left: -1050, behavior: "smooth" });
    };

    const scrollRight = () => {
        homeImageRef.current.scrollBy({ left: 1050, behavior: "smooth" });
    };

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
                    <div id="home-image-carousel">
                        <button className="arrow-btn" onClick={scrollLeft}>&lt;</button>
                        <div id="home-image" ref={homeImageRef}>
                            <img src="src\assets\home-image-1.jpg" alt="" />
                            <img src="src\assets\home-image-2.jpg" alt="" />
                        </div>
                        <button className="arrow-btn" onClick={scrollRight}>&gt;</button>
                    </div>
                    <ProductCarousel sectionTitle={'Best sellers'} products={mostViewedProducts}></ProductCarousel>
                    <ProductCarousel sectionTitle={'Discounts'} products={productsWithDiscounts}></ProductCarousel>
                </div>
            </div>
            <Footer></Footer>
        </>
    )
}