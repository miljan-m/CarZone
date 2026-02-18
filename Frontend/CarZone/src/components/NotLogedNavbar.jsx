import React from 'react'
import { UilCarSideview } from '@iconscout/react-unicons'
import { UilSignout } from '@iconscout/react-unicons'
import { UilRegistered } from '@iconscout/react-unicons'
import { Link } from 'react-router-dom'

const NotLogedNavbar = () => {
    return (
        <header>
            <Link to={"/offers"}><UilCarSideview className="icon" color="#52af50" /> Offers </Link>
            <h2>CarZone</h2>
            <div className="account-icons-div">
                <Link to={"/login"}>Login <UilSignout className="icon" color="#52af50" /></Link>
                <Link to={"/register"}>Register <UilRegistered className="icon" color="#52af50" /></Link>
            </div>


        </header>
    )
}

export default NotLogedNavbar