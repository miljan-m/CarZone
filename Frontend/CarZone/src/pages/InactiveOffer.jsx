import React, { useEffect, useState } from 'react'
import Footer from '../components/Footer'
import LogedNavbar from '../components/LogedNavbar'
import axios from 'axios'
import OfferCard from '../components/OfferCard'
import '../styles/InactiveOffer.css'

const InactiveOffer = () => {

    const user = JSON.parse(localStorage.getItem('user'))
    const [offers, setOffers] = useState([])


    useEffect(() => {
        axios.get('http://localhost:5047/listings').then((response) => {
            let tempOffers = response.data
            let filteredOffers = tempOffers.filter(o => o.user.email === user.email && o.listingStatus === 'Sold')
            setOffers(filteredOffers)
        }).catch(function (error) {
            console.log(error)
        })
    }, [])

    return (

        <div className="inactive-offers-wrapper">
            <LogedNavbar />
            <h1>Inactive Offers</h1>

            {
                offers.map((o, index) => (<OfferCard key={index} offer={o} isLiked={false} />))
            }
            <Footer />
        </div>


    )
}

export default InactiveOffer