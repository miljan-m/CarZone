import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../styles/SettingsCard.css';

const SettingsCard = ({
  clickedIcon,
  brands,
  models,
  addBrandToState,
  addModelToState,
  removeBrandFromState,
  removeModelFromState
}) => {

  const [brandName, setBrandName] = useState('');
  const [selectedBrand, setSelectedBrand] = useState(null);
  const [modelName, setModelName] = useState('');
  const [errors, setErrors] = useState({});

  useEffect(() => {
    if (brands.length > 0 && !selectedBrand) {
      setSelectedBrand(brands[0]);
    }
  }, [brands, selectedBrand]);

  const validateBrand = () => {
    let e = {};
    if (!brandName.trim()) e.brandName = "Brand name is required";
    else if (!/^[A-Z]/.test(brandName)) e.brandName = "Brand name must start with capital letter";
    setErrors(e);
    return Object.keys(e).length === 0;
  };

  const validateModel = () => {
    let e = {};
    if (!modelName.trim()) e.modelName = "Model name is required";
    else if (!/^[A-Z]/.test(modelName)) e.modelName = "Model name must start with capital letter";
    setErrors(e);
    return Object.keys(e).length === 0;
  };

  const handleBrandAdd = () => {
    if (!validateBrand()) return;
    axios.post('http://localhost:5047/brands', { brandName }, {
      headers: { Authorization: `Bearer ${localStorage.getItem('token')}` }
    }).then(response => {
      addBrandToState(response.data);
      setBrandName('');
      setErrors({});
    }).catch(err => console.log(err));
  };

  const handleModelAdd = () => {
    if (!selectedBrand) {
      setErrors({ modelName: "Select a brand first" });
      return;
    }
    if (!validateModel()) return;
    axios.post(`http://localhost:5047/models/${selectedBrand.brandId}`, { modelName }, {
      headers: { Authorization:  `Bearer ${localStorage.getItem('token')}` }
    }).then(response => {
      addModelToState(response.data);
      setModelName('');
      setErrors({});
    }).catch(err => console.log(err));
  };

  const handleBrandDelete = async (brandId) => {
    try {
      await axios.delete(`http://localhost:5047/brands/${brandId}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      });

      removeBrandFromState(brandId);
    } catch (error) {
      console.error("Error deleting brand:", error);
    }
  };

  const handleModelDelete = async (modelId) => {
    try {
      await axios.delete(`http://localhost:5047/models/${modelId}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      });

      removeModelFromState(modelId);
    } catch (error) {
      console.error("Error deleting model:", error);
    }
  };

  return (
    <div className="settings-card">

      {clickedIcon === 'brands' && (
        <>
          <h2>Brands</h2>

          <div className="input-group">
            <div className="input-wrapper">
              <input
                type="text"
                placeholder="Brand name"
                value={brandName}
                onChange={(e) => { setBrandName(e.target.value); setErrors({}); }}
              />
              {errors.brandName && <span className="error-message">{errors.brandName}</span>}
            </div>
            <button className="addBrandButton" onClick={handleBrandAdd}>Add</button>
          </div>

          <div className="list-container">
            {brands.map(b => (
              <div key={b.brandId} className="list-item">
                <span>{b.brandName}</span>
                <button className="deleteBrandButton" onClick={() => handleBrandDelete(b.brandId)}>Delete</button>
              </div>
            ))}
          </div>
        </>
      )}

      {clickedIcon === 'models' && (
        <>
          <h2>Models</h2>

          <select
            className="brand-select"
            value={selectedBrand?.brandName || ''}
            onChange={(e) => setSelectedBrand(brands.find(b => b.brandName === e.target.value))}
          >
            <option value='' disabled>Select Brand</option>
            {brands.map(b => <option key={b.brandId} value={b.brandName}>{b.brandName}</option>)}
          </select>

          <div className="input-group">
            <div className="input-wrapper">
              <input
                type="text"
                placeholder="Model name"
                value={modelName}
                onChange={(e) => { setModelName(e.target.value); setErrors({}); }}
              />
              {errors.modelName && <span className="error-message">{errors.modelName}</span>}
            </div>
            <button className="addModelButton" onClick={()=>handleModelAdd()}>Add</button>
          </div>

          <div className="list-container">
            {models.filter(m => m.brandName === selectedBrand?.brandName).map(m => (
              <div key={m.modelId} className="list-item">
                <span>{m.modelName}</span>
                <button className="deleteModelButton" onClick={() => handleModelDelete(m.modelId)}>Delete</button>
              </div>
            ))}
          </div>
        </>
      )}

    </div>
  );
};

export default SettingsCard;