import React, { useState } from 'react'
import '../styles/OfferCard.css'
import { useNavigate } from 'react-router-dom';


const OfferCard = (props) => {
    var navigate = useNavigate();
    var isLiked = props.isLiked

    var modelName = props.offer.model.modelName
    var brandName = props.offer.model.brandName
    var carPrice = props.offer.price
    var productionYear = props.offer.productionYear
    var images = props.offer.images
    var status = props.offer.listingStatus
    var user = JSON.parse(localStorage.getItem('user'))


    const likeUnlikeOffer = () => {
        props.handleLikedPosts({ ...props.offer, isLiked: !isLiked, whoLiked: user.email });
    }

    return (
        <div className="card-container">
            <img src={`http://localhost:5047/${images[0].imageUrl}`} alt="" onClick={() => navigate("/offer-details", { state: { offer: props.offer } })} />
            <div className="car-info-container">
                <span><strong>{brandName} {modelName}</strong></span>
                <span>{carPrice}€</span>
                <span>{productionYear}</span>
                <div className="like-and-status-container">
                    {
                        status == "Active" ? <span>✔️ <strong>Active</strong></span> : <span>❌<strong>Sold</strong></span>
                    }
                    {
                        user?.email !== props.offer.user.email && (
                            <button onClick={likeUnlikeOffer}>
                                {isLiked ? '❤️' : '🤍'}
                            </button>
                        )
                    }
                </div>
            </div>
        </div>
    )
}



export default OfferCard