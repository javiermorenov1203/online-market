import Header from "../components/Header";
import { useParams } from 'react-router-dom';
import { useEffect, useState } from "react";
import { fetchProduct } from "../api/productApi";
import { fetchMostViewedProducts } from "../api/productApi";
import HomeCarousel from "../components/HomeCarousel";
import "./ProductDetailPage.css";

export default function ProductDetailPage() {

    const { id } = useParams();

    const baseUrl = import.meta.env.VITE_API_BASE
    const [product, setProduct] = useState({})
    const [mostViewedProducts, setMostViewedProducts] = useState([])


    useEffect(() => {
        const loadProduct = async () => {
            var data = await fetchProduct(id)
            setProduct(data.product)
            data = await fetchMostViewedProducts()
            setMostViewedProducts(data)
        };
        loadProduct();
    }, []);

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
                                            <div>
                                                <img src={baseUrl + i} alt={product.name} loading="lazy" />
                                            </div>) : (<></>)
                                    ))}
                                </div>
                                <div id="selected-product-image">
                                    <img src={baseUrl + product.images?.[0]} alt={product.name} loading="lazy" />
                                </div>
                            </div>
                            <div id="product-info">
                                <h3>{product.name}</h3>
                                <p>{!!product.stock ? 'Out of Stock' : 'In Stock'}</p>
                                <p>{product.description}</p>
                            </div>
                            <div id="purchase-panel">
                                <p className="product-card-base-price">{!!product.discount ? 'USD ' + product.basePrice?.toFixed(2) : ''}</p>
                                <p className="product-card-price">USD {product.finalPrice?.toFixed(2)}</p>
                                <p className="product-card-discount">{!!product.discount ? product.discount + '% OFF' : ''}</p>
                                <button>Add to cart</button>
                                <button>Buy now</button>
                            </div>
                        </div>
                    </div>
                    <div className="product-section">
                        <h3 className="section-title">Most popular products</h3>
                        <HomeCarousel products={mostViewedProducts}></HomeCarousel>
                    </div>
                </div>
            </div>

        </>
    )
}