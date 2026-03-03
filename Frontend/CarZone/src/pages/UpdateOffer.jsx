import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import axios from 'axios';
import '../styles/UpdateOffer.css';
import LogedNavBar from '../components/LogedNavbar';
import Footer from '../components/Footer';


const listingStatusOptions = ['Active', 'Sold'];
const transmissionOptions = ['Manual', 'Automatic'];
const bodyTypeOptions = ['Sedan', 'Hatchback', 'SUV', 'Coupe', 'Convertible'];
const engineTypeOptions = ['Petrol', 'Diesel', 'Electric', 'Hybrid'];

const UpdateOffer = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { offer } = location.state || {};
  const [errors, setErrors] = useState({});

  const [formData, setFormData] = useState({
    AdditionalDescription: offer?.additionalDescription || '',
    BodyType: offer?.bodyType || 'Sedan',
    EngineType: offer?.engineType || 'Petrol',
    FuelConsuption: offer?.fuelConsuption || 0,
    ListingStatus: offer?.listingStatus || 'Active',
    Mileage: offer?.mileage || 0,
    Transmission: offer?.transmission || 'Manual',
    Price: offer?.price || 0,
    ProductionYear: offer?.productionYear || 2024,
  });

  const handleChange = (e) => {
    const { name, value, type } = e.target;
    const val = type === 'number' ? parseFloat(value) : value;
    setFormData({
      ...formData,
      [name]: val,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) return;
    const token = localStorage.getItem('token');

    const formDataToSend = new FormData();
    for (let key in formData) {
      formDataToSend.append(key, formData[key]);
    }
    try {
      await axios.patch(`http://localhost:5047/listings/${offer.listingId}`, formDataToSend, {
        headers: { Authorization: `Bearer ${token}` },
      });
      navigate('/offers');
    } catch (error) {
      console.error(error);
    }
  };
  const validate = () => {
    let tempErrors = {};
    const currentYear = new Date().getFullYear();
    var isValid = true;

    if (formData.Price <= 0) {
      isValid = false;
      tempErrors.Price = "Price must be greater than 0."
    };

    if (formData.Mileage <= 0) {
      tempErrors.Mileage = "Mileage cannot be 0."
      isValid = false;

    };

    if (formData.Mileage > 1500000) {
      isValid = false;

      tempErrors.Mileage = "Mileage cannot exceed 1,500,000 km."
    };

    if (formData.ProductionYear < 1885 || formData.ProductionYear > currentYear + 1) {
      tempErrors.ProductionYear = `Year must be between 1885 and ${currentYear + 1}.`;
      isValid = false;

    }

    if (!formData.FuelConsuption || formData.FuelConsuption <= 0) {
      tempErrors.FuelConsuption = "Fuel consumption must be greater than 0.";
      isValid = false;

    } else if (formData.FuelConsuption > 50) {
      tempErrors.FuelConsuption = "Consumption cannot exceed 50L/100km.";
      isValid = false;

    }

    setErrors(tempErrors);
    return isValid
  };
  return (
    <div className="update-offer-wrapper">
      <LogedNavBar />
      <div className="update-offer-container">
        <h2>
          {offer?.model?.brandName} {offer?.model?.modelName}
        </h2>

        <form onSubmit={handleSubmit} className="update-form">
          <div className="form-grid">
            <div className="form-group">
              <label>Price (€)</label>
              <input className={errors.Price ? 'input-error' : ''} type="number" name="Price" value={formData.Price} min={0} onChange={handleChange} />
              {errors.Price && <span className="error-message">{errors.Price}</span>}
            </div>

            <div className="form-group">
              <label>Mileage (km)</label>
              <input className={errors.Mileage ? 'input-error' : ''} type="number" name="Mileage" value={formData.Mileage} min={0} onChange={handleChange} required />
              {errors.Mileage && <span className="error-message">{errors.Mileage}</span>}
            </div>

            <div className="form-group">
              <label>Production year</label>
              <input className={errors.ProductionYear ? 'input-error' : ''} type="number" name="ProductionYear" value={formData.ProductionYear} onChange={handleChange} required />
              {errors.ProductionYear && <span className="error-message">{errors.ProductionYear}</span>}
            </div>

            <div className="form-group">
              <label>Fuel Consumption (L/100km)</label>
              <input className={errors.FuelConsuption ? 'input-error' : ''} type="number" step="0.1" name="FuelConsuption" value={formData.FuelConsuption} min={0} onChange={handleChange} />
              {errors.FuelConsuption && <span className="error-message">{errors.FuelConsuption}</span>}
            </div>

            <div className="form-group">
              <label>Engine Type</label>
              <select name="EngineType" value={formData.EngineType} onChange={handleChange}>
                {engineTypeOptions.map((e) => (
                  <option key={e} value={e}>{e}</option>
                ))}
              </select>
            </div>

            <div className="form-group">
              <label>Transmission</label>
              <select name="Transmission" value={formData.Transmission} onChange={handleChange}>
                {transmissionOptions.map((t) => (
                  <option key={t} value={t}>{t}</option>
                ))}
              </select>
            </div>

            <div className="form-group">
              <label>Body Type</label>
              <select name="BodyType" value={formData.BodyType} onChange={handleChange}>
                {bodyTypeOptions.map((b) => (
                  <option key={b} value={b}>{b}</option>
                ))}
              </select>
            </div>

            <div className="form-group">
              <label>Status</label>
              <select name="ListingStatus" value={formData.ListingStatus} onChange={handleChange}>
                {listingStatusOptions.map((s) => (
                  <option key={s} value={s}>{s}</option>
                ))}
              </select>
            </div>
          </div>

          <div className="form-group full-width">
            <label>Additional Description</label>
            <textarea
              name="AdditionalDescription"
              value={formData.AdditionalDescription}
              onChange={handleChange}
              rows="5"
            />
          </div>

          <div className="form-buttons">
            <button type="button" className="btn-cancel" onClick={() => navigate(-1)}>Odustani</button>
            <button type="submit" className="btn-submit">Save</button>
          </div>
        </form>
      </div>
      <Footer />
    </div>
  );
};

export default UpdateOffer;
