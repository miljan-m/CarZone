import React, { useEffect, useState } from 'react'
import Footer from '../components/Footer'
import LogedNavbar from '../components/LogedNavbar'
import axios from 'axios'
import OfferCard from '../components/OfferCard'
import '../styles/ActiveOffer.css'
const ActiveOffer = () => {

    const user = JSON.parse(localStorage.getItem('user'))
    const [offers, setOffers] = useState([])
    const offersPerPage = 4;
    const [currentPage, setCurrentPage] = useState(1)

    const totalPages = Math.ceil(offers.length / offersPerPage)
    const indexOfLastOffer = currentPage * offersPerPage;
    const indexOfFirstOffer = indexOfLastOffer - offersPerPage;
    const paginatedOffers = offers.slice(indexOfFirstOffer, indexOfLastOffer)

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
            <h1>Active Offers</h1>

            <div className="Active-offers-grid">

                {paginatedOffers.length > 0 ? (
                    paginatedOffers.map((o, index) => (
                        <OfferCard key={index} offer={o} isLiked={false} />
                    ))
                ) : (
                    <p className="no-inactive-offers">No active offers</p>
                )}
            </div>
            {totalPages != 0 ?
                <div className="pagination-div">
                    <button
                        className="pagination-btn"
                        disabled={currentPage === 1}
                        onClick={() => setCurrentPage(currentPage - 1)}
                    >
                        &laquo; Previous
                    </button>

                    <div className="page-indicator">
                        <span className="current-page">{currentPage}</span>
                        <span className="separator">/</span>
                        <span className="total-pages">{totalPages}</span>
                    </div>

                    <button
                        className="pagination-btn"
                        disabled={currentPage === totalPages || totalPages == 0}
                        onClick={() => setCurrentPage(currentPage + 1)}
                    >
                        Next &raquo;
                    </button>
                </div> : null}
            <Footer />
        </div>
    )
}

export default ActiveOffer