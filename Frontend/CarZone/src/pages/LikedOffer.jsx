import React, { useState, useEffect } from 'react';
import OfferCard from '../components/OfferCard';
import LogedNavBar from '../components/LogedNavbar';
import Footer from '../components/Footer';
import "../styles/LikedOffer.css"

const LikedOffers = () => {
    const [likedOffers, setLikedOffers] = useState([]);
    const user = JSON.parse(localStorage.getItem('user'))


    const offersPerPage = 4;
    const [currentPage, setCurrentPage] = useState(1)

    const totalPages = Math.ceil(likedOffers.length / offersPerPage)
    const indexOfLastOffer = currentPage * offersPerPage;
    const indexOfFirstOffer = indexOfLastOffer - offersPerPage;
    const paginatedOffers = likedOffers.slice(indexOfFirstOffer, indexOfLastOffer)


    useEffect(() => {
        const saved = localStorage.getItem('likedoffers');
        if (saved) {
            setLikedOffers(JSON.parse(saved).filter(o => o.whoLiked === user.email));
        }
    }, []);

    const handleRemoveLike = (offer) => {
        const updated = likedOffers.filter(o => o.listingId !== offer.listingId);
        setLikedOffers(updated);
        localStorage.setItem('likedoffers', JSON.stringify(updated));
    };

    return (
        <div className="liked-offers-wrapper">
            <LogedNavBar />
            <h1>Liked Offers</h1>
            <div className="my-liked-offers-div">
                {paginatedOffers.length > 0 ? (
                    paginatedOffers.map((o, index) => (
                        <OfferCard
                            key={index}
                            offer={o}
                            handleLikedPosts={handleRemoveLike}
                            isLiked={o.isLiked}
                        />
                    ))
                ) : (
                    <p>No Liked Offers</p>
                )}
            </div>
            {
                paginatedOffers.length > 0 ?
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
                            disabled={currentPage === totalPages}
                            onClick={() => setCurrentPage(currentPage + 1)}
                        >
                            Next &raquo;
                        </button>
                    </div> : null
            }

            <Footer />
        </div>
    );
};

export default LikedOffers;