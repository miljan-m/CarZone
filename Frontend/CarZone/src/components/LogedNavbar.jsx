import React, { useState } from 'react'
import { useContext } from 'react'
import { AuthContext } from '../Authentication/AuthContext'
import { UilCarSideview } from '@iconscout/react-unicons'
import { UilUserCircle } from '@iconscout/react-unicons'
import { UilSignout } from '@iconscout/react-unicons'
import { UilSetting } from '@iconscout/react-unicons'
import { UilCreateDashboard } from '@iconscout/react-unicons'
import { UilCommentDots } from '@iconscout/react-unicons'
import { UilUser, UilHeart, UilCheckCircle, UilTimesCircle } from '@iconscout/react-unicons'
import { Link } from 'react-router-dom'

const LogedNavBar = () => {
    const { handleLogout } = useContext(AuthContext)
    const user = JSON.parse(localStorage.getItem('user'))
    const [isMenuOpen, setIsMenuOpen] = useState(false)

    const openCloseMenu = () => setIsMenuOpen(!isMenuOpen)

    return (
        < header >
            <div className="offer-icons-div">
                <Link to={"/offers"}><UilCarSideview className="icon" color="#52af50" /> Offers </Link>
                <Link to={"/create-offer"}> <UilCreateDashboard className="icon" color="#52af50" />Create Offer</Link>
                <Link to={"/chat"}><UilCommentDots className="icon" color="#52af50" />Chat</Link>
            </div>

            <h2>CarZone</h2>
            <div className="account-icons-div">
                <div className="dropdown-container">
                    <button className='account-button' onClick={() => openCloseMenu()}>Account <UilUserCircle className="icon" color="#52af50" /></button>
                    {isMenuOpen && (
                        <div className="dropdown-menu">
                            <Link to="/account" onClick={() => setIsMenuOpen(false)}>
                                <UilUser size="20" /> Account Settings
                            </Link>
                            <Link to="/liked-offers" onClick={() => setIsMenuOpen(false)}>
                                <UilHeart size="20" /> Liked Offers
                            </Link>
                            <Link to="/active-offers" onClick={() => setIsMenuOpen(false)}>
                                <UilCheckCircle size="20" /> Active Offers
                            </Link>
                            <Link to="/inactive-offers" onClick={() => setIsMenuOpen(false)}>
                                <UilTimesCircle size="20" /> Inactive Offers
                            </Link>
                        </div>
                    )}
                </div>

                {user.roles.includes('Admin') ? <Link to={'/settings'}>Settings <UilSetting className="icon" color="#52af50" /></Link> : null}
                <a href="/login" onClick={() => handleLogout()}>Logout <UilSignout className="icon" color="#52af50" /></a>
            </div>
        </header >
    )
}

export default LogedNavBar