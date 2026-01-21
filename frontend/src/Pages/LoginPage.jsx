import LoginHeader from "../components/LoginHeader"
import "./LoginPage.css"

export default function LoginPage() {

    return (
        <>
            <LoginHeader></LoginHeader>
            <div id="login-form-container">
                <h2>Log into your account</h2>
                <p>Don't have an account? Click <a href="/register">here</a> to Sign up.</p>
                <form id="login-form" action="submit">
                    <label>Email
                        <input type="email" name="email" className="field"/>
                    </label>
                    <label>Password
                        <input type="password" name="password" className="field"/>
                    </label>
                    <input type="submit" value="Log in" />
                </form>
            </div>
        </>
    )
}