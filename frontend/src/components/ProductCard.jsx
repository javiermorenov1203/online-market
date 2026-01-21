import "./ProductCard.css"

export default function ProductCard() {
    return (
        <div className="product-card">
            <img src="src\assets\playstation-5-pro.jpg" alt="" />
            <div>
                <p className="title">Lorem ipsum dolor sit amet, consectetur adipiscing elit Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>
                <p>USD 1500</p>
            </div>
        </div>
    )
}