import { useRef } from "react"
import LoginHeader from "../components/LoginHeader"
import { userLogin } from "../api/userApi"
import { useNavigate } from "react-router-dom"
import "./LoginPage.css"

export default function LoginPage() {

    const navigate = useNavigate()

    const emailRef = useRef(null)
    const passwordRef = useRef(null)

    const handleLogin = async (e) => {
        e.preventDefault()
        const data = await userLogin(emailRef.current.value, passwordRef.current.value);
        navigate('/');
    }

    return (
        <>
            <LoginHeader></LoginHeader>
            <div id="login-form-container">
                <h2>Log into your account</h2>
                <p>Don't have an account? Click <a href="/register">here</a> to sign up.</p>
                <form id="login-form" onSubmit={handleLogin}>
                    <label>Email
                        <input type="email" name="email" className="field" ref={emailRef}/>
                    </label>
                    <label>Password
                        <input type="password" name="password" className="field" ref={passwordRef}/>
                    </label>
                    <input type="submit" value="Log in"/>
                </form>
            </div>
        </>
    )
}