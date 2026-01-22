import { useNavigate } from 'react-router-dom';
import LoginHeader from "../components/LoginHeader"
import { useRef } from 'react';
import { userLogin, userRegister } from '../api/userApi';
import "./RegisterPage.css"

export default function RegisterPage() {

    const navigate = useNavigate();

    const emailRef = useRef(null)
    const passwordRef = useRef(null)
    const confirmPasswordRef = useRef(null)
    const firstNameRef = useRef(null)
    const middleNameRef = useRef(null)
    const lastNameRef = useRef(null)
    const secondLastNameRef = useRef(null)

    const handleSubmit = async (e) => {
        e.preventDefault()
        if (passwordRef.current.value !== confirmPasswordRef.current.value) {
            console.log("passowrds do not match")
            return
        }
        const data = await userRegister(emailRef.current.value, passwordRef.current.value,
            firstNameRef.current.value, middleNameRef.current.value,
            lastNameRef.current.value, secondLastNameRef.current.value)
        await userLogin(emailRef.current.value, passwordRef.current.value)
        navigate("/")
    }

    return (
        <>
            <LoginHeader></LoginHeader>
            <form id="register-form" onSubmit={handleSubmit}>
                <h3>Account Information</h3>
                <div id="account-info">
                    <label>Email
                        <input type="email" name="email" className="field" ref={emailRef} />
                    </label>
                    <label>Password
                        <input type="password" name="password" className="field" ref={passwordRef} />
                    </label>
                    <label>Confirm password
                        <input type="password" name="password" className="field" ref={confirmPasswordRef} />
                    </label>
                </div>
                <h3>Personal Information</h3>
                <div id="personal-info">
                    <label>First name
                        <input type="text" name="firstName" className="field" ref={firstNameRef} />
                    </label>
                    <label>Middle name
                        <input type="text" name="middleName" className="field" ref={middleNameRef} />
                    </label>
                    <label>Last name
                        <input type="text" name="lastName" className="field" ref={lastNameRef} />
                    </label>
                    <label>Second last name
                        <input type="text" name="secondLastName" className="field" ref={secondLastNameRef} />
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