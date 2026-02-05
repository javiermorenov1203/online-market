import { useSearchParams } from "react-router-dom";
import Header from "../components/Header";
import Footer from "../components/Footer";
import "./SearchProductPage.css"
import { useEffect, useState } from "react";
import { fetchSearchProducts } from "../api/productApi";
import ProductCard from "../components/ProductCard";

export default function SearchProductPage() {

    const [searchParams] = useSearchParams();
    const name = searchParams.get("q");

    const [products, setProducts] = useState([])

    useEffect(() => {
        const loadProducts = async () => {
            var data = await fetchSearchProducts(name)
            setProducts(data)
        };
        loadProducts();
    }, [name]);

    return (
        <>
            <Header></Header>
            <div className="page">
                <div className="content">
                    <div id="search-results-upper-section">
                        <div id="upper-left">
                            <p>Searching: {name}</p>
                            <p>Results: {products.length}</p>
                        </div>
                        <div id="upper-right">
                            <label>Filter: </label>
                            <select name="filter" id="results-filter" className="field">
                                <option value="most-relevant">Most relevant</option>
                                <option value="most-relevant">Highest price</option>
                                <option value="most-relevant">Lowest price</option>
                            </select>
                        </div>
                    </div>
                    <div id="search-results-lower-section">
                        <div id="search-filter-panel">
                            <p><strong>Price: </strong></p>
                            <div className="checkbox-group">
                                <div className="checkbox-container">
                                    <input type="radio" id="$50" name="price"></input>
                                    <label for="$50">Up to $50</label>
                                </div>
                                <div className="checkbox-container">
                                    <input type="radio" id="$50-250" name="price"></input>
                                    <label for="$50-250">$50 to $250</label>
                                </div>
                                <div className="checkbox-container">
                                    <input type="radio" id="$250-400" name="price"></input>
                                    <label for="$250-400">$250 to $400</label>
                                </div>
                                <div className="checkbox-container">
                                    <input type="radio" id="$400-above" name="price"></input>
                                    <label for="$400-above">$400 & above</label>
                                </div>
                            </div>
                            <p><strong>Brand: </strong></p>
                            <div className="checkbox-group">
                                <div className="checkbox-container">
                                    <input type="checkbox" id="Adidas" name="brand"></input>
                                    <label for="Adidas">Adidas</label>
                                </div>
                                <div className="checkbox-container">
                                    <input type="checkbox" id="Nike" name="brand"></input>
                                    <label for="Nike">Nike</label>
                                </div>
                                <div className="checkbox-container">
                                    <input type="checkbox" id="Acme" name="brand"></input>
                                    <label for="Acme">Acme</label>
                                </div>
                            </div>
                            <p><strong>Discounts: </strong></p>
                            <div className="checkbox-group">
                                <div className="checkbox-container">
                                    <input type="radio" id="discounts" name="brand"></input>
                                    <label for="discounts">Discounts</label>
                                </div>
                            </div>
                        </div>
                        <div id="search-results-panel">
                            <div id="search-results-wrapper">
                                {(products.length !== 0) ?
                                    products.map(p => (
                                        <ProductCard product={p}></ProductCard>
                                    ))
                                    : (<>
                                        <ProductCard />
                                        <ProductCard />
                                        <ProductCard />
                                        <ProductCard />
                                        <ProductCard />
                                    </>
                                    )}
                            </div>
                            <div id="pagination-buttons">
                                <button className="arrow-btn">&lt;</button>
                                <div id="current-page">1</div>
                                <button className="arrow-btn">&gt;</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <Footer></Footer>
        </>
    )
}