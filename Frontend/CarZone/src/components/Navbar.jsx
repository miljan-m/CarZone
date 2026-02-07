import React from 'react'
import '../styles/Navbar.css'
import { UilCarSideview } from '@iconscout/react-unicons'
import { UilUserCircle } from '@iconscout/react-unicons'

const Navbar = () => {
  return (
    <header>


      <a href=""><UilCarSideview className="icon" color="#52af50" /> Offers </a>
      <h2>CarZone</h2>
      <a href="">Account <UilUserCircle className="icon" color="#52af50" /></a>

    </header>
  )
}

export default Navbar