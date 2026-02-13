import React from 'react'
import '../styles/OfferCard.css'

const OfferCard = (props) => {

    
    return (
        <div className="card-container">
            <img src={`http://localhost:5047/${props.images[0].imageUrl}`} alt="" />
            <div className="car-info-container">
                <p>{props.brandName} {props.modelName}</p>
                <p>{props.carPrice}€</p>
                <p>{props.productionYear}</p>
            </div>
        </div>
    )
}

export default OfferCard