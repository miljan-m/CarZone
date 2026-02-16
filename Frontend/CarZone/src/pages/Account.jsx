import React, { useContext } from 'react'
import '../styles/Account.css'
import Footer from '../components/Footer'
import NotLogedNavbar from '../components/NotLogedNavbar'
import LogedNavBar from '../components/LogedNavbar'
import { Navigate, useNavigate } from 'react-router-dom'
import UpdateAccount from './UpdateAccount'


export const Account = () => {
    const user = JSON.parse(localStorage.getItem('user'))
    const token = localStorage.getItem('token')
    const navigate = useNavigate();
    return (
        <div className="account-wrapper">
            {token ? <LogedNavBar /> : <NotLogedNavbar />}
            <div className="account-informations-div">
                <div className="account-header">
                    <span>Account Details</span>
                </div>
                <div className="account-details-div">
                    <span>First Name: {user.firstName}</span>
                    <span>Last Name: {user.lastName}</span>
                    <span>Phone: {user.phone}</span>
                    <span>Email: {user.email}</span>
                </div>
                <div className="account-buttons-div">
                    <button className='updateButton' onClick={() => navigate('/update-account')}>Update</button>
                </div>
            </div>
            <Footer />
        </div >
    )
}
