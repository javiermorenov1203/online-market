import { useRef } from "react";
import ProductCard from "./ProductCard";
import "./HomeCarousel.css"

export default function HomeCarousel({ products }) {

    const panelRef = useRef(null);

    const scrollLeft = () => {
        panelRef.current.scrollBy({ left: -220, behavior: "smooth" });
    };

    const scrollRight = () => {
        panelRef.current.scrollBy({ left: 220, behavior: "smooth" });
    };

    return (
        <div className="carousel-wrapper">
            <button className="arrow-btn" onClick={scrollLeft}>&lt;</button>
            <div className="product-panel" ref={panelRef}>
                {products.map(p => (
                    <ProductCard key={p.id} product={p} />
                ))}
            </div>
            <button className="arrow-btn" onClick={scrollRight}>&gt;</button>
        </div>
    )
}