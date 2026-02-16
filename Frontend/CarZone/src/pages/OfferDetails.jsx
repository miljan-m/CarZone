import React, { useState } from 'react';
import '../styles/OfferDetails.css'
import { useLocation } from 'react-router-dom';
import Footer from '../components/Footer';
import LogedNavBar from '../components/LogedNavbar';
import NotLogedNavbar from '../components/NotLogedNavbar';

const OfferDetails = () => {

  const location = useLocation();
  var offer = location.state.offer;
  const [currentImage, setCurrentImage] = useState(offer.images[0].imageUrl);
  const token = localStorage.getItem('token')

  return (
    <div className="offer-details-wrapper">
      {token ? <LogedNavBar /> : <NotLogedNavbar />}
      <div className="offer-details-container">

        <div className="images-div">
          {offer.images && offer.images.length > 0 ? (
            <img src={`http://localhost:5047/${currentImage}`} alt={offer.model.modelName} />
          ) : (
            <div className="placeholder-img">Nema slike</div>
          )}
          <div className="image-thumbnails">
            {offer.images.map((img, index) => (
              <img key={img.imageId} src={`http://localhost:5047/${img.imageUrl}`} alt="thumbnail" className="thumb" onClick={() => setCurrentImage(img.imageUrl)} />
            ))}
          </div>
        </div>

        <div className="offer-details">
          <div className="header-info">
            <h1>{offer.model.brandName} {offer.model.modelName}</h1>
            <span className="price-tag">{offer.price} €</span>
          </div>

          <div className="specs-grid">
            <div className="spec-item">
              <span className="label">Production year: </span>
              <span className="value">{offer.productionYear}</span>
            </div>
            <div className="spec-item">
              <span className="label">Mileage: </span>
              <span className="value">{offer.mileage.toLocaleString()} km</span>
            </div>
            <div className="spec-item">
              <span className="label">Fuel: </span>
              <span className="value">{offer.engineType}</span>
            </div>
            <div className="spec-item">
              <span className="label">Transmission: </span>
              <span className="value">{offer.transmission}</span>
            </div>
            <div className="spec-item">
              <span className="label">Body: </span>
              <span className="value">{offer.bodyType}</span>
            </div>
            <div className="spec-item">
              <span className="label">Fuel Consumption: </span>
              <span className="value">{offer.fuelConsuption} L/100km</span>
            </div>
          </div>

          <div className="description-section">
            <h3>Offer details</h3>
            <p>{offer.additionalDescription}</p>
          </div>

          <div className="seller-info">
            <h3>Seller contact</h3>
            <p><strong>Name: </strong> {offer.user.firstName} {offer.user.lastName}</p>
            <p><strong>Phone: </strong> {offer.user.phone}</p>
            <p><strong>Email:</strong> {offer.user.email}</p>
          </div>
        </div>
      </div>
      <Footer />
    </div>

  );
};

export default OfferDetails;