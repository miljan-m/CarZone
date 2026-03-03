import React, { useState } from 'react'
import Footer from '../components/Footer'
import '../styles/Register.css'
import { UilKeySkeletonAlt, UilEnvelopeAlt, UilPhone, UilHome, UilUser } from '@iconscout/react-unicons'
import { useNavigate } from 'react-router-dom';
import axios from 'axios'
import NotLogedNavbar from '../components/NotLogedNavbar'
import LogedNavBar from '../components/LogedNavbar'

const Register = () => {

    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [address, setAddress] = useState('')
    const [password, setPassword] = useState('')
    const [errors, setErrors] = useState({})

    const navigate = useNavigate();
    const token = localStorage.getItem('token')

    const handleRegister = async () => {
        try {
            const response = await axios.post('http://localhost:5047/user/register', {
                firstName,
                lastName,
                email,
                phone,
                address,
                password
            })

            console.log(response.status + ' ' + response.statusText)
            navigate('/login')
        } catch (error) {
            console.log(error)
        }
    }

    const validate = () => {
        let tempErrors = {};
        let isValid = true;

        if (!firstName) {
            tempErrors.firstName = "First name cannot be empty";
            isValid = false;
        } else if (!/^[A-Z][a-zA-Z]*$/.test(firstName)) {
            tempErrors.firstName = "Must start with capital letter";
            isValid = false;
        }

        if (!lastName) {
            tempErrors.lastName = "Last name cannot be empty";
            isValid = false;
        } else if (!/^[A-Z][a-zA-Z]*$/.test(lastName)) {
            tempErrors.lastName = "Must start with capital letter";
            isValid = false;
        }

        if (!address) {
            tempErrors.address = "Address cannot be empty";
            isValid = false;
        } else if (!/^[A-Z].*/.test(address)) {
            tempErrors.address = "Must start with capital letter";
            isValid = false;
        }

        if (!email) {
            tempErrors.email = "Email cannot be empty";
            isValid = false;
        } else if (!/\S+@\S+\.\S+/.test(email)) {
            tempErrors.email = "Invalid email format";
            isValid = false;
        }

        if (!phone) {
            tempErrors.phone = "Phone cannot be empty";
            isValid = false;
        } else if (!(phone.startsWith("06") && /^\d+$/.test(phone) && phone.length === 10)) {
            tempErrors.phone = "Must start with 06 and contain 10 digits";
            isValid = false;
        }

        if (!password) {
            tempErrors.password = "Password cannot be empty";
            isValid = false;
        } else if (!/[A-Z]/.test(password)) {
            tempErrors.password = "Must contain capital letter";
            isValid = false;
        } else if (!/\d/.test(password)) {
            tempErrors.password = "Must contain number";
            isValid = false;
        } else if (!/[!@#$%^&*]/.test(password)) {
            tempErrors.password = "Must contain special character";
            isValid = false;
        }

        setErrors(tempErrors);
        return isValid;
    }

    const onRegisterClick = () => {
        if (validate()) {
            handleRegister()
        }
    }

    return (
        <div className='register-wrapper'>
            {token ? <LogedNavBar /> : <NotLogedNavbar />}

            <div className='register-container'>
                <div className='register-header'>Register</div>

                <div className="register-input">

                    <div className="input-group">
                        <div className="input-row">
                            <UilUser className="input-icon" />
                            <input type="text" placeholder='First Name' onChange={(e) => setFirstName(e.target.value)} />
                        </div>
                        {errors.firstName && <span className="error-messages">{errors.firstName}</span>}
                    </div>

                    <div className="input-group">
                        <div className="input-row">
                            <UilUser className="input-icon" />
                            <input type="text" placeholder='Last Name' onChange={(e) => setLastName(e.target.value)} />
                        </div>
                        {errors.lastName && <span className="error-messages">{errors.lastName}</span>}
                    </div>

                    <div className="input-group">
                        <div className="input-row">
                            <UilPhone className="input-icon" />
                            <input type="text" placeholder='Phone Number' onChange={(e) => setPhone(e.target.value)} />
                        </div>
                        {errors.phone && <span className="error-messages">{errors.phone}</span>}
                    </div>

                    <div className="input-group">
                        <div className="input-row">
                            <UilHome className="input-icon" />
                            <input type="text" placeholder='Street' onChange={(e) => setAddress(e.target.value)} />
                        </div>
                        {errors.address && <span className="error-messages">{errors.address}</span>}
                    </div>

                    <div className="input-group">
                        <div className="input-row">
                            <UilEnvelopeAlt className="input-icon" />
                            <input type="text" placeholder='Email' onChange={(e) => setEmail(e.target.value)} />
                        </div>
                        {errors.email && <span className="error-messages">{errors.email}</span>}
                    </div>

                    <div className="input-group">
                        <div className="input-row">
                            <UilKeySkeletonAlt className="input-icon" />
                            <input type="password" placeholder='Password' onChange={(e) => setPassword(e.target.value)} />
                        </div>
                        {errors.password && <span className="error-messages">{errors.password}</span>}
                    </div>

                    <button className='button' onClick={onRegisterClick}>Register</button>

                </div>

                <div className='register-footer'>
                    Already have an account? <a href='/login'>Login</a>
                </div>
            </div>

            <Footer />
        </div>
    )
}

export default Register