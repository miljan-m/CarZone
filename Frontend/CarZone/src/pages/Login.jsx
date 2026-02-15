import React, { useContext, useState } from 'react'
import '../styles/Login.css'
import Footer from '../components/Footer'
import { UilKeySkeletonAlt } from '@iconscout/react-unicons'
import { UilEnvelopeAlt } from '@iconscout/react-unicons'
import axios from 'axios'
import { AuthContext } from '../Authentication/AuthContext'
import NotLogedNavbar from '../components/NotLogedNavbar'
const Login = () => {

    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    const { user, token, handleLogin, handleLogout } = useContext(AuthContext)

    function handleEmailChange(e) {
        setEmail(e.target.value);
    }

    function handlePasswordChange(e) {
        setPassword(e.target.value);
    }

    //{
    // "firstName": "Marjan",
    // "lastName": "Ristic",
    // "email": "mr@gmail.com",
    //  "phone": "0625374511",
    // "address": "Kozaracka 15",
    // "password": "MyPass1@"
    //}
    return (
        <>
            <NotLogedNavbar />
            <div className='login-container'>
                <div className='login-header'>Login</div>
                <div className="login-input">
                    <div className="email-div">
                        <UilEnvelopeAlt className="input-icon" />
                        <input type="text" placeholder='Email' onChange={handleEmailChange} />
                    </div>
                    <div className="password-div">
                        <UilKeySkeletonAlt className="input-icon" />
                        <input type="password" placeholder='Password' onChange={handlePasswordChange} />
                    </div>
                </div>
                <div className="login-buttons">
                    <button className='button' onClick={() => handleLogin(email, password)}>Login</button>
                </div>
                <div className='login-footer'>Not registered? <a href="./register">Create an account</a></div>
            </div>
            <Footer />
        </>

    )
}

export default Login