import Header from "../components/Header";
import { useParams } from 'react-router-dom';
import { useEffect, useRef, useState } from "react";
import { fetchProduct } from "../api/productApi";
import { fetchMostViewedProducts } from "../api/productApi";
import ProductCarousel from "../components/ProductCarousel";
import "./ProductDetailPage.css";

export default function ProductDetailPage() {

    const { id } = useParams();

    const baseUrl = import.meta.env.VITE_API_BASE
    const [product, setProduct] = useState({})
    const [selectedImageUrl, setSelectedImageUrl] = useState()
    const [mostViewedProducts, setMostViewedProducts] = useState([])

    useEffect(() => {
        const loadProduct = async () => {
            var data = await fetchProduct(id)
            var product = data.product
            setProduct(product)
            setSelectedImageUrl(baseUrl + product.images[0])
            data = await fetchMostViewedProducts()
            setMostViewedProducts(data)
        };
        loadProduct();
    }, [id]);

    return (
        <>
            <Header></Header>
            <div className="page">
                <div id="product-page-content">
                    <div id="product-panel-wrapper">
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
                                <p>{product.description}</p>
                            </div>
                            <div id="purchase-panel">
                                <div id="price-wrapper">
                                    <p className="product-card-base-price">{!!product.discount ? 'USD ' + product.basePrice?.toFixed(2) : ''}</p>
                                    <div id='final-price'>
                                        <p className="product-card-price">USD {product.finalPrice?.toFixed(2)}</p>
                                        <p className="product-card-discount">{!!product.discount ? product.discount + '% OFF' : ''}</p>
                                    </div>
                                    <p style={{ color: (!!product.stock) ? 'rgb(255, 78, 78)' : 'green' }}>
                                        {!!product.stock ? 'Out of Stock' : 'In Stock'}
                                    </p>
                                </div>
                                <label>Quanity
                                    <input type="number" />
                                </label>
                                <div id="button-container">
                                    <button>Add to cart</button>
                                    <button className="red-btn">Buy now</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <ProductCarousel sectionTitle={'Related products'} products={mostViewedProducts}></ProductCarousel>
                </div>
            </div>

        </>
    )
}