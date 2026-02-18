import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../styles/SettingsCard.css'

const SettingsCard = ({ clickedIcon, brands, models, addBrandToState, addModelToState, removeBrandFromState, removeModelFromState }) => {

  const [brandName, setBrandName] = useState('')
  const [selectedBrand, setSelectedBrand] = useState()
  const [modelName, setModelName] = useState('');


  useEffect(() => {
    if (brands.length > 0) {
      setSelectedBrand(brands[0])
    }
  }, [brands])



  const handleBrandAdd = () => {
    axios.post('http://localhost:5047/brands',
      { brandName },
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      }
    ).then((response) => {
      addBrandToState(response.data)
    }).catch((error) => {
      console.log(error.response.data)
    })
  }


  const handleBrandDelete = (brandId) => {
    axios.delete(`http://localhost:5047/brands/${brandId}`,
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      }
    ).then((response) => {
      removeBrandFromState(brandId)
    }).catch((error) => {
      console.log(error)
    })
  }

  const handleModelAdd = () => {
    axios.post(`http://localhost:5047/models/${selectedBrand.brandId}`,
      { modelName },
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      }
    ).then((response) => {
      addModelToState(response.data)
    }).catch((error) => {
      console.log(error)
    })
  }

  const handleModelDelete = (model) => {
    axios.delete(`http://localhost:5047/models/${model.modelId}`,
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        }
      }
    ).then((response) => {
      removeModelFromState(model.modelId)
    }).catch((error) => {
      console.log(error)
    })
  }

  if (clickedIcon == 'brands') return (
    <div className="brands-settings-div">
      <h2>Brands</h2>
      <div className="input-group">
        <input className='brand-input' type="text" placeholder='Brand' onChange={(e) => setBrandName(e.target.value)} />

        <button className='addBrandButton' onClick={() => handleBrandAdd()}>Add</button>
      </div>
      {
        brands.map((b, index) =>
          <div key={index}>
            {b.brandName}
            <button className='dedleteBrandButton' onClick={() => handleBrandDelete(b.brandId)}>Delete</button>
          </div>)
      }
    </div>
  )

  if (clickedIcon == 'models') return (
    <div className="models-settings-div">
      <h2>Models</h2>
      <select name="select-model" value={selectedBrand.brandName} onChange={(e) => {
        const brand = brands.find(b => b.brandName == e.target.value)
        setSelectedBrand(brand)
      }}>
        <option value={''} disabled={true}>Brand</option>
        {
          brands.map((b, index) => <option key={index} value={b.brandName} > {b.brandName} </option>)
        }
      </select>
      <div className="input-group">
        <input type="text" placeholder='Model' onChange={(e) => setModelName(e.target.value)} />
        <button className='addModelButton' onClick={() => handleModelAdd()}>Add</button>
      </div>
      {
        models.filter(m => m.brandName == selectedBrand.brandName).map((m,index)=>
          <div key={index}>
            {m.modelName}
            <button className='deleteModelButton' onClick={() => handleModelDelete(m)}>Delete</button>
          </div>
        )

      }


    </div>
  )
};

export default SettingsCard;