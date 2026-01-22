import { Link } from "react-router";
import "./Header.css"

export default function Header() {

    return (
        <header className="main-header">
            <div className="header-container">
                <div className="header-center">
                    <Link to="/" id="page-title">Online Market</Link>
                    <input type="search" placeholder="Search an item..." />
                </div>
                <div className="header-right">
                    {localStorage.getItem('token') ?
                        (
                            <Link to="/" className="header-button">Account</Link>
                        ) : (
                            <>
                                <Link to="/register" className="header-button">Sign up</Link>
                                <Link to="/login" className="header-button">Login</Link>
                            </>
                        )
                    }

                    <Link className="header-button">Cart</Link>
                </div>
            </div>
        </header>
    )
}