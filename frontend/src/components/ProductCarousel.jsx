import { useRef, useState } from "react";
import ProductCard from "./ProductCard";
import "./ProductCarousel.css"

export default function ProductCarousel({ sectionTitle, products }) {

    const panelRef = useRef(null);
    const [isDisabled, setIsDisabled] = useState(false);

    const scrollLeft = () => {
        panelRef.current.scrollBy({ left: -212, behavior: "smooth" });
    };

    const scrollRight = () => {
        setIsDisabled(true);
        panelRef.current.scrollBy({ left: 212, behavior: "smooth" });
        setTimeout(() => {
            setIsDisabled(false);
        }, 300);
    };

    return (
        <div className="product-section">
            <h3 className="section-title">{sectionTitle}</h3>
            <div className="carousel-wrapper">
                <button className="arrow-btn" onClick={scrollLeft}>&lt;</button>
                <div className="product-panel" ref={panelRef}>
                    {products.map(p => (
                        <ProductCard key={p.id} product={p} />
                    ))}
                </div>
                <button className="arrow-btn" onClick={scrollRight} disabled={isDisabled}>&gt;</button>
            </div>
        </div>
    )
}