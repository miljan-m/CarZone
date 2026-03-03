import React, { useContext, useState } from 'react'
import '../styles/Login.css'
import Footer from '../components/Footer'
import { UilKeySkeletonAlt, UilEnvelopeAlt } from '@iconscout/react-unicons'
import axios from 'axios'
import { AuthContext } from '../Authentication/AuthContext'
import NotLogedNavbar from '../components/NotLogedNavbar'

const Login = () => {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [errors, setErrors] = useState({ email: '', password: '' })

    const { handleLogin } = useContext(AuthContext)

    const handleEmailChange = (e) => setEmail(e.target.value);
    const handlePasswordChange = (e) => setPassword(e.target.value);

    const validateForm = () => {
        let tempErrors = { email: '', password: '' };
        let isValid = true;

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!email) {
            tempErrors.email = "Email address cannot be empty";
            isValid = false;
        } else if (!emailRegex.test(email)) {
            tempErrors.email = "Format: example@example.com";
            isValid = false;
        }

        if (!password) {
            tempErrors.password = "Password cannot be empty";
            isValid = false;
        } else {
            if (!/[A-Z]/.test(password)) {
                tempErrors.password = "Needs one capital letter";
                isValid = false;
            } else if (!/\d/.test(password)) {
                tempErrors.password = "Needs one number";
                isValid = false;
            } else if (!/[!@#$%^&*]/.test(password)) {
                tempErrors.password = "Needs one special character (!@#$%^&*)";
                isValid = false;
            }
        }

        setErrors(tempErrors);
        return isValid;
    };

    const onLoginClick = () => {
        if (validateForm()) {
            handleLogin(email, password);
        }
    };

    return (
        <div className="login-page-wrapper">
            <NotLogedNavbar />
            
            <div className='login-container' onKeyDown={e => e.key === "Enter" && onLoginClick()}>
                <div className='login-header'>Login</div>
                
                <div className="login-form">
                    {/* Email Grupa */}
                    <div className="input-group">
                        <div className="input-field-row">
                            <UilEnvelopeAlt className="input-icon" />
                            <input 
                                type="text" 
                                placeholder='Email' 
                                onChange={handleEmailChange} 
                                value={email}
                            />
                        </div>
                        {errors.email && <span className="error-message">{errors.email}</span>}
                    </div>

                    {/* Password Grupa */}
                    <div className="input-group">
                        <div className="input-field-row">
                            <UilKeySkeletonAlt className="input-icon" />
                            <input 
                                type="password" 
                                placeholder='Password' 
                                onChange={handlePasswordChange} 
                                value={password}
                            />
                        </div>
                        {errors.password && <span className="error-message">{errors.password}</span>}
                    </div>

                    <div className="login-buttons">
                        <button className='button' onClick={onLoginClick}>Login</button>
                    </div>
                </div>

                <div className='login-footer'>
                    Not registered? <a href="./register">Create an account</a>
                </div>
            </div>

            <Footer />
        </div>
    )
}

export default Login