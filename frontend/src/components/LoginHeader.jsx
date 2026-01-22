import { Link } from "react-router-dom"
import "./Header.css"

export default function Header() {

    return (
        <header className="main-header">
            <Link to="/" id="page-title">Online Market</Link>
        </header>
    )
}