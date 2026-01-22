import "./ProductCard.css"

export default function ProductCard({ product }) {
    return (
        <div className="product-card">
            <div className="product-image-container">
                <img src={import.meta.env.VITE_API_BASE + product.image1} alt={product.name} loading="lazy" />
            </div>
            <div>
                <p className="title">{product.name}</p>
                <p>USD {product.price}</p>
            </div>
        </div>
    )
}