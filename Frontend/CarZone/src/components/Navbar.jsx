import React, { useContext } from 'react'
import '../styles/Navbar.css'
import { UilCarSideview } from '@iconscout/react-unicons'
import { UilUserCircle } from '@iconscout/react-unicons'
import { UilSignout } from '@iconscout/react-unicons'
import { AuthContext } from '../Authentication/AuthContext'

const Navbar = () => {
  const { handleLogout } = useContext(AuthContext)
  return (
    <header>
      <a href="/offers"><UilCarSideview className="icon" color="#52af50" /> Offers </a>
      <h2>CarZone</h2>
      <div className="account-div">
        <a href="/account">Account <UilUserCircle className="icon" color="#52af50" /></a>
        <a href="/login" onClick={() => handleLogout()}>Logout <UilSignout className="icon" color="#52af50" /></a>
      </div>


    </header>
  )
}

export default Navbar