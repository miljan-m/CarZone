import React from 'react'
import { useContext } from 'react'
import { AuthContext } from '../Authentication/AuthContext'
import { UilCarSideview } from '@iconscout/react-unicons'
import { UilUserCircle } from '@iconscout/react-unicons'
import { UilSignout } from '@iconscout/react-unicons'
import { UilPlusCircle } from '@iconscout/react-unicons'
import { UilCreateDashboard } from '@iconscout/react-unicons'

const LogedNavBar = () => {
    const { handleLogout } = useContext(AuthContext)
    return (
        <header>
            <div className="offer-icons-div">
                <a href="/offers"><UilCarSideview className="icon" color="#52af50" /> Offers </a>
                <a href="/create-offer"> <UilCreateDashboard className="icon" color="#52af50" />Create Offer</a>
            </div>

            <h2>CarZone</h2>
            <div className="account-icons-div">
                <a href="/account">Account <UilUserCircle className="icon" color="#52af50" /></a>
                <a href="/login" onClick={() => handleLogout()}>Logout <UilSignout className="icon" color="#52af50" /></a>

            </div>
        </header>
    )
}

export default LogedNavBar