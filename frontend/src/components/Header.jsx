import { Link, useNavigate } from "react-router";
import { useState } from "react";
import "./Header.css"

export default function Header() {

    const [query, setQuery] = useState("");
    const navigate = useNavigate();

    const handleKeyDown = (e) => {
        if (e.key === "Enter") {
            navigate(`/search?q=${encodeURIComponent(query)}`);
        }
    };

    return (
        <header className="main-header">
            <div className="header-container">
                <div className="header-center">
                    <Link to="/" id="page-title">Online Market</Link>
                    <input
                        type="search"
                        placeholder="Search an item..."
                        value={query}
                        onChange={(e) => setQuery(e.target.value)}
                        onKeyDown={handleKeyDown}
                    />
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