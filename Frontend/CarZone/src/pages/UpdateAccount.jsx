import React from 'react'
import { UilPhone } from '@iconscout/react-unicons'
import { UilHome } from '@iconscout/react-unicons'
import { UilUser } from '@iconscout/react-unicons'
import '../styles/UpdateAccount.css'
import LogedNavBar from '../components/LogedNavbar'
import NotLogedNavbar from '../components/NotLogedNavbar'
import Footer from '../components/Footer'
import axios from 'axios'

const UpdateAccount = () => {

    const token = localStorage.getItem('token')
    const userJSON = localStorage.getItem('user')
    const user = JSON.parse(userJSON)
    console.log(user)

    const handleFirstNameChange = (e) => {
        e.target.value == user.firstName ? {} : user.firstName = e.target.value
    }

    const handleLastNameChange = (e) => {
        e.target.value == user.lastName ? {} : user.lastName = e.target.value

    }
    const handleAddressChange = (e) => {
        e.target.value == user.address ? {} : user.address = e.target.value

    }
    const handlePhoneNumberChange = (e) => {
        e.target.value == user.phone ? {} : user.phone = e.target.value


    }

    const handleAccountUpdate = () => {
        localStorage.removeItem('user')
        localStorage.setItem('user', JSON.stringify(user))
        const u=localStorage.getItem('user')
        const updatedUser=JSON.parse(u);

        axios.patch(`http://localhost:5047/user/update/${updatedUser.userId}`, updatedUser)
            .then((response) => {
                console.log(response)
            }).catch((error => {
                console.log(error)
            }))
    }

    return (
        <div className="update-wrapper">
            {token ? <LogedNavBar /> : <NotLogedNavbar />}
            <div className="update-account-div">
                <div className="headedr-div"></div>
                <div className="update-form-div">
                    <div className="first-name-div">
                        <UilUser className="input-icon" />
                        <input type="text" placeholder={`${user.firstName}`} onChange={(e) => handleFirstNameChange(e)} />
                    </div>
                    <div className="last-name-div">
                        <UilUser className="input-icon" />
                        <input type="text" placeholder={`${user.lastName}`} onChange={(e) => handleLastNameChange(e)} />
                    </div>
                    <div className="phone-div">
                        <UilPhone className="input-icon" />
                        <input type="text" placeholder={`${user.phone}`} onChange={(e) => handlePhoneNumberChange(e)} />
                    </div>
                    <div className="address-div">
                        <UilHome className="input-icon" />
                        <input type="text" placeholder={`${user.address}`} onChange={(e) => handleAddressChange(e)} />
                    </div>
                </div>
                <div className="button-div">
                    <button className='updateButton' onClick={() => handleAccountUpdate()}>Update</button>
                </div>
            </div>
            <Footer />
        </div>
    )
}

export default UpdateAccount