import React, { useState } from 'react'
import Navbar from '../components/Navbar'
import Footer from '../components/Footer'
import '../styles/Register.css'
import { UilKeySkeletonAlt } from '@iconscout/react-unicons'
import { UilEnvelopeAlt } from '@iconscout/react-unicons'
import { UilPhone } from '@iconscout/react-unicons'
import { UilHome } from '@iconscout/react-unicons'
import { UilUser } from '@iconscout/react-unicons'
import { useNavigate } from 'react-router-dom';
import axios from 'axios'
const Register = () => {

    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [address, setAddress] = useState('')
    const [password, setPassword] = useState('')
    const navigate = useNavigate();
    /*
    public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password{get;set;}
    */

    const handleFirstNameChange = (e) => {
        setFirstName(e.target.value)
    }

    const handleLastNameChange = (e) => {
        setLastName(e.target.value)
    }
    const handleEmailChange = (e) => {
        setEmail(e.target.value)
    }
    const handlePhoneChange = (e) => {
        setPhone(e.target.value)
    }
    const handleAddressChange = (e) => {
        setAddress(e.target.value)
    }
    const handlePasswordChange = (e) => {
        setPassword(e.target.value)
    }

    const handleRegister = async () => {
        await axios.post('http://localhost:5047/user/register', {
            firstName,
            lastName,
            email,
            phone,
            address,
            password
        }).then((response) => {
            console.log(response.status + ' ' + response.statusText)
            navigate('/login')

        }).catch(function (error) {
            console.log(error)
        })

    }

    return (
        <>
            <div className='register-wrapper'>
                <Navbar />
                <div className='register-container'>
                    <div className='register-header'>Register</div>
                    <div className="register-input">
                        <div className="first-name-div">
                            <UilUser className="input-icon" />
                            <input type="text" placeholder='First Name' onChange={handleFirstNameChange} />
                        </div>
                        <div className="last-name-div">
                            <UilUser className="input-icon" />
                            <input type="text" placeholder='Last Name' onChange={handleLastNameChange} />
                        </div>
                        <div className="phone-div">
                            <UilPhone className="input-icon" />
                            <input type="text" placeholder='Phone Number' onChange={handlePhoneChange} />
                        </div>
                        <div className="address-div">
                            <UilHome className="input-icon" />
                            <input type="text" placeholder='Street' onChange={handleAddressChange} />
                        </div>
                        <div className="email-div">
                            <UilEnvelopeAlt className="input-icon" />
                            <input type="text" placeholder='Email' onChange={handleEmailChange} />
                        </div>
                        <div className="password-div">
                            <UilKeySkeletonAlt className="input-icon" />
                            <input type="password" placeholder='Password' onChange={handlePasswordChange} />
                        </div>

                    </div>
                    <div className="register-buttons">
                        <button className='button' onClick={handleRegister}>Register</button>
                    </div>
                    <div className='register-footer'>Already have an account? <a href='./Login'> Login </a></div>
                </div>
                <Footer/>
            </div>
        </>
    )
}

export default Register