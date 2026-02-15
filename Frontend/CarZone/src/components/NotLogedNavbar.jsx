import React from 'react'
import { UilCarSideview } from '@iconscout/react-unicons'
import { UilSignout } from '@iconscout/react-unicons'
import { UilRegistered } from '@iconscout/react-unicons'

const NotLogedNavbar = () => {
    return (
        <header>
            <a href="/offers"><UilCarSideview className="icon" color="#52af50" /> Offers </a>
            <h2>CarZone</h2>
            <div className="account-div">
                <a href="/login">Login <UilSignout className="icon" color="#52af50" /></a>
                <a href="/register">Register <UilRegistered className="icon" color="#52af50" /></a>
            </div>


        </header>
    )
}

export default NotLogedNavbar