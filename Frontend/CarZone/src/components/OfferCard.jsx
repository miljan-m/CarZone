import React from 'react'
import '../styles/OfferCard.css'
import { useNavigate } from 'react-router-dom';

const OfferCard = (props) => {
    var navigate = useNavigate();

    var modelName = props.offer.model.modelName
    var brandName = props.offer.model.brandName
    var carPrice = props.offer.price
    var productionYear = props.offer.productionYear
    var images = props.offer.images

    return (
        <div className="card-container">
            <img src={`http://localhost:5047/${images[0].imageUrl}`} alt="" onClick={() => navigate("/offer-details", { state: { offer: props.offer } })} />
            <div className="car-info-container">
                <span><strong>{brandName} {modelName}</strong></span>
                <span>{carPrice}€</span>
                <span>{productionYear}</span>
            </div>
        </div>
    )
}



export default OfferCard