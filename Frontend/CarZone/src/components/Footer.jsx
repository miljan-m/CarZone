import React from 'react'
import '../styles/Footer.css'
const Footer = () => {
  return (
    <footer>
      <div className="footer-content">
        <p>&copy;  CarZone. All rights reserved</p>
        <div className="footer-links">
          <a href="#privacy" >Privacy policy</a>
          <a href="#terms" >About us</a>
          <a href="#contact" >Contacts</a>
        </div>
      </div>
    </footer>
  )
}

export default Footer