import React from 'react'
import { useContext } from 'react'
import { AuthContext } from '../Authentication/AuthContext'
import { UilCarSideview } from '@iconscout/react-unicons'
import { UilUserCircle } from '@iconscout/react-unicons'
import { UilSignout } from '@iconscout/react-unicons'
import { UilSetting } from '@iconscout/react-unicons'
import { UilCreateDashboard } from '@iconscout/react-unicons'
import { Link } from 'react-router-dom'

const LogedNavBar = () => {
    const { handleLogout } = useContext(AuthContext)
    const user = JSON.parse(localStorage.getItem('user'))

    return (
        < header >
            <div className="offer-icons-div">
                <Link to={"/offers"}><UilCarSideview className="icon" color="#52af50" /> Offers </Link>
                <Link to={"/create-offer"}> <UilCreateDashboard className="icon" color="#52af50" />Create Offer</Link>
            </div>

            <h2>CarZone</h2>
            <div className="account-icons-div">
                <Link to={"/account"}>Account <UilUserCircle className="icon" color="#52af50" /> </Link>
                {user.roles.includes('Admin') ? <Link to={'/settings'}>Settings <UilSetting className="icon" color="#52af50" /></Link> : null}
                <a href="/login" onClick={() => handleLogout()}>Logout <UilSignout className="icon" color="#52af50" /></a>
            </div>
        </header >
    )
}

export default LogedNavBar