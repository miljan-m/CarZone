import React, { useState, useEffect } from 'react';
import OfferCard from '../components/OfferCard';
import LogedNavBar from '../components/LogedNavbar';
import Footer from '../components/Footer';
import "../styles/LikedOffer.css"

const LikedOffers = () => {
    const [likedOffers, setLikedOffers] = useState([]);
    const user = JSON.parse(localStorage.getItem('user'))
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
            <div className="my-offers-div">
                {likedOffers.length > 0 ? (
                    likedOffers.map((o, index) => (
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
            <Footer />
        </div>
    );
};

export default LikedOffers;