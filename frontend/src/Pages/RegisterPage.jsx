import { useNavigate } from 'react-router-dom';
import LoginHeader from "../components/LoginHeader"
import "./RegisterPage.css"

export default function RegisterPage() {

    const navigate = useNavigate();

    return (
        <>
            <LoginHeader></LoginHeader>
            <form id="register-form" action="submit">
                <h3>Account Information</h3>
                <div id="account-info">
                    <label>Email
                        <input type="email" name="email" className="field" />
                    </label>
                    <label>Password
                        <input type="password" name="password" className="field" />
                    </label>
                    <label>Confirm password
                        <input type="password" name="password" className="field" />
                    </label>
                </div>
                <h3>Personal Information</h3>
                <div id="personal-info">
                    <label>First name
                        <input type="text" name="firstName" className="field" />
                    </label>
                    <label>Middle name
                        <input type="text" name="middleName" className="field" />
                    </label>
                    <label>Last name
                        <input type="text" name="lastName" className="field" />
                    </label>
                    <label>Second last name
                        <input type="text" name="secondLastName" className="field" />
                    </label>
                </div>
                <div id="register-footer">
                    <button type="button" onClick={() => navigate(-1)}>Cancel</button>
                    <input type="submit" value="Register" />
                </div>
            </form>
        </>
    )
}