import { NavLink } from "react-router";
import "./Header.css"

export default function Header() {

    return (
        <header className="main-header">
            <div className="header-container">
                <div className="header-center">
                    <p>Online Market</p>
                    <input type="search" placeholder="Search an item..."/>
                </div>
                <div className="header-right">
                    <NavLink to="/register" className="header-button">Sign up</NavLink>
                    <NavLink to="/login" className="header-button">Login</NavLink>
                    <NavLink className="header-button">Cart</NavLink>
                </div>
            </div>
        </header>
    )
}