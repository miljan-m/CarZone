import React, { useEffect, useState } from 'react'
import Footer from '../components/Footer'
import LogedNavbar from '../components/LogedNavbar'
import axios from 'axios'
import OfferCard from '../components/OfferCard'
import '../styles/ActiveOffer.css'
const ActiveOffer = () => {

    const user = JSON.parse(localStorage.getItem('user'))
    const [offers, setOffers] = useState([])


    useEffect(() => {
        axios.get('http://localhost:5047/listings').then((response) => {
            let tempOffers = response.data
            let filteredOffers = tempOffers.filter(o => o.user.email === user.email && o.listingStatus === 'Active')
            setOffers(filteredOffers)
        }).catch(function (error) {
            console.log(error)
        })
    }, [])

    return (

        <div className="active-offers-wrapper">
            <LogedNavbar />
            <div className="Active-offers-grid">
            <h1>Active Offers</h1>

                {offers.length > 0 ? (
                    offers.map((o, index) => (
                        <OfferCard key={index} offer={o} isLiked={false} />
                    ))
                ) : (
                    <p className="no-inactive-offers">No active offers</p>
                )}
            </div>
            <Footer />
        </div>


    )
}

export default ActiveOffer