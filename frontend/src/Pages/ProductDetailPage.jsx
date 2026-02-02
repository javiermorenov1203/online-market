import Header from "../components/Header";
import { useParams } from 'react-router-dom';
import { useEffect, useState } from "react";
import { fetchProduct } from "../api/productApi";
import { fetchProductsByCategory } from "../api/productApi";
import ProductCarousel from "../components/ProductCarousel";
import Footer from "../components/Footer";
import "./ProductDetailPage.css";

export default function ProductDetailPage() {

    const { id } = useParams();

    const baseUrl = import.meta.env.VITE_API_BASE
    const [product, setProduct] = useState({})
    const [selectedImageUrl, setSelectedImageUrl] = useState()
    const [relatedProducts, setRelatedProducts] = useState([])

    useEffect(() => {
        const loadProduct = async () => {
            const data = await fetchProduct(id)
            const product = data.product

            setProduct(product)
            setSelectedImageUrl(baseUrl + product.images[0])

            let related = await fetchProductsByCategory(product.categoryId)

            // filtrar EXCLUYENDO el producto actual
            related = related.filter(p => p.id !== product.id)

            setRelatedProducts(related)
        };
        loadProduct();
    }, [id]);

    return (
        <>
            <Header></Header>
            <div className="page">
                <div className="content">
                        <div id="product-panel">
                            <div id="product-images">
                                <div id="product-images-sidebar">
                                    {product.images?.map(i => (
                                        !!i ? (
                                            <div onClick={() => setSelectedImageUrl(baseUrl + i)}>
                                                <img src={baseUrl + i} alt={product.name} loading="lazy" />
                                            </div>) : (<></>)
                                    ))}
                                </div>
                                <div id="selected-product-image">
                                    <img src={selectedImageUrl} alt={product.name} loading="lazy" />
                                </div>
                            </div>
                            <div id="product-info">
                                <h3>{product.name}</h3>
                                <p><strong>Publisher:</strong> <a href="">Javier Moreno</a></p>
                                <p id="stock-availability"><strong>Availabilty:</strong>
                                    <span className={product.stock != 0 ? "in-stock" : "no-stock"}>
                                        {product.stock != 0 ? " In Stock" : " Out of stock"}
                                    </span>
                                </p>
                                <p><strong>Rating:</strong> [placeholder info]</p>
                                <p><strong>Brand:</strong> Best Brand Ever</p>
                                <p><strong>Color:</strong> [placeholder info]</p>
                                <p><strong>Weight:</strong> [placeholder info]</p>
                            </div>
                            <div id="purchase-panel">
                                <div id="price-wrapper">
                                    <div id='base-price'>
                                        <p className="product-card-base-price">{!!product.discount ? 'USD ' + product.basePrice?.toFixed(2) : ''}</p>
                                        <p className="product-card-discount">{!!product.discount ? product.discount + '% OFF' : ''}</p>
                                    </div>
                                    <p className="product-card-price">USD {product.finalPrice?.toFixed(2)}</p>
                                </div>
                                <label>Quanity
                                    <input className="field" type="number" defaultValue={1} min={1} />
                                </label>
                                <div id="button-container">
                                    <button>Add to cart</button>
                                    <button className="red-btn">Buy now</button>
                                </div>
                            </div>
                        </div>
                    <div id="description-section">
                        <h3>Description</h3>
                        <p>{product.description}</p>
                    </div>
                    <div id="reviews-section">
                        <h3>Reviews</h3>
                        <p>This product has no reviews yet.</p>
                    </div>
                    <ProductCarousel sectionTitle={'Related products'} products={relatedProducts}></ProductCarousel>
                </div>
            </div>
            <Footer></Footer>
        </>
    )
}