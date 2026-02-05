import "./ProductCard.css"
import { useNavigate } from "react-router-dom"

export default function ProductCard({ product }) {

    const navigate = useNavigate()

    return (
        <div className="product-card" onClick={() => navigate(`/product/${product.id}`)}>
            <div className="product-image-container">
                {product ?
                    <img src={import.meta.env.VITE_API_BASE + product.images?.[0]} alt={product.name} loading="lazy" />
                    :
                    <div className="image-loading"></div>
                }
            </div>
            <div className="product-card-info">
                {product ?
                    <>
                        <p className="product-card-title">{product.name}</p>
                        <div className="product-card-price-container">
                            <p className="product-card-price">USD {product.finalPrice.toFixed(2)}</p>
                            <p className="product-card-discount">{!!product.discount ? product.discount + '% OFF' : ''}</p>
                        </div>
                        <p className="product-card-base-price">{!!product.discount ? 'USD ' + product.basePrice.toFixed(2) : ''}</p>
                    </>
                    :
                    <div className="info-loading"></div>
                }
            </div>
        </div>
    )
}