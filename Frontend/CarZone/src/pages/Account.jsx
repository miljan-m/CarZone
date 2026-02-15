import React, { useContext } from 'react'
import '../styles/Account.css'
import Footer from '../components/Footer'
import NotLogedNavbar from '../components/NotLogedNavbar'
import LogedNavBar from '../components/LogedNavbar'


export const Account = () => {
    const user = JSON.parse(localStorage.getItem('user'))
    const token = localStorage.getItem('token')
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
                    <button id='updateAccountButton'>Update Account</button>
                </div>
            </div>
            <Footer />
        </div>
    )
}
