import React from 'react'
import Navbar from '../components/Navbar'
import Footer from '../components/Footer'
import { useState, useEffect } from 'react'
import axios from 'axios'
import '../styles/CreateOffer.css'
import OfferCard from '../components/OfferCard'
import NotLogedNavbar from '../components/NotLogedNavbar'
import LogedNavBar from '../components/LogedNavbar'

const CreateOffer = () => {
    const [brands, setBrands] = useState([])
    const [selectedBrand, setSelectedBrand] = useState("")
    const [models, setModels] = useState([])
    const [selectedModel, setSelectedModel] = useState("")
    const [bodyType, setBodyTypes] = useState([])
    const [selectedBodyType, setSelectedBodyType] = useState("")
    const [engineType, setEngineType] = useState([])
    const [selectedEngineType, setSelectedEngineType] = useState("")
    const [transmissions, setTransmissions] = useState([])
    const [selectedTransmission, setSelectedTransmission] = useState("")
    const [price, setPrice] = useState(0)
    const [year, setYear] = useState("")
    const [mileage, setMileage] = useState("")
    const [fuelConsumption, setFuelConsumption] = useState(0)
    const [details, setDetails] = useState("")
    const [images, setImages] = useState([])
    const [offers, setOffers] = useState([])
    const token = localStorage.getItem('token')
    const logedUser = JSON.parse(localStorage.getItem('user'))
    const [myOffers, setMyOffers] = useState([])
    const [errors, setErrors] = useState({});
    const minYear = 1900
    const maxYear = new Date().getFullYear();
    const years = [];

    for (let i = maxYear; i >= minYear; i--) {
        years.push(i)
    }

    useEffect(() => {
        axios.get("http://localhost:5047/brands").then(function (response) {
            setBrands(response.data)
        }).catch(function (error) {
            console.log(error)
        })
    }, [])
    //models for brand
    useEffect(() => {
        if (selectedBrand == "") return
        axios.get(`http://localhost:5047/brands/${selectedBrand}/models`).then((response) => {
            setModels(response.data)
        }).catch(function (error) {
            console.log(error)
        })

    }, [selectedBrand])

    useEffect(() => {
        axios.get("http://localhost:5047/bodyTypes").then((response) => {
            setBodyTypes(response.data)
        }).catch(function (error) {
            console.log(error)
        })
    }, [])

    useEffect(() => {
        axios.get("http://localhost:5047/engineTypes").then((response) => {
            setEngineType(response.data)
        }).catch(function (error) {
            console.log(error)
        })
    }, [])

    useEffect(() => {
        axios.get("http://localhost:5047/transmission").then((response) => {
            setTransmissions(response.data)
        }).catch(function (error) {
            console.log(error)
        })
    }, [])



    const handleOfferCreation = async () => {
        if (!validate()) {
            console.log("Validation failed")
            return
        }
        const formData = new FormData();

        const transmissionEnum = { Manual: 0, Automatic: 1 };
        const bodyTypeEnum = { Hatchback: 0, Coupe: 1, Sedan: 2, Minivan: 3, SUV: 4, Van: 4, Pickup: 5, Wagon: 6 };
        const engineTypeEnum = { Petrol: 0, Diesel: 1, Electric: 2, Hybrid: 3, TNG: 4 }

        formData.append("ProductionYear", Number(year));
        formData.append("Price", Number(price));
        formData.append("Mileage", Number(mileage));
        formData.append("FuelConsuption", Number(fuelConsumption));
        formData.append("AdditionalDescription", details);
        formData.append("ModelId", Number(selectedModel));
        formData.append("Transmission", transmissionEnum[selectedTransmission]);
        formData.append("BodyType", bodyTypeEnum[selectedBodyType]);
        formData.append("EngineType", engineTypeEnum[selectedEngineType]);


        for (let i = 0; i < images.length; i++) {
            formData.append("Images", images[i]);
        }

        for (let pair of formData.entries()) {
            console.log(pair[0], pair[1]);
        }

        axios.post(`http://localhost:5047/listings/${logedUser.userId}`, formData,
            {
                headers: {
                    "Content-Type": "multipart/form-data"
                }
            }
        ).then(function (response) {
            handleOfferFetching();
        }).catch(function (error) {
            console.log(error)
        })
    }
    //offers
    const handleOfferFetching = () => {
        axios.get('http://localhost:5047/listings').then((response) => {
            setOffers(response.data)
            setMyOffers(response.data.filter(o => o.user.email == logedUser.email))
            console.log(response.data)
        }).catch(function (error) {
            console.log(error)
        })
    }

    useEffect(() => {
        handleOfferFetching()
    }, [])

    const validate = () => {
        let isValid = true;
        let tempErrors = {};
        const currentYear = new Date().getFullYear();

        if (!selectedBodyType) {
            isValid = false;
            tempErrors.bodyType = "Please select a body type.";
        }

        if (!selectedEngineType) {
            isValid = false;
            tempErrors.engineType = "Please select an engine type.";
        }
        if (!selectedTransmission) {
            isValid = false;
            tempErrors.transmission = "Please select a transmission type.";
        }

        if (!fuelConsumption || fuelConsumption <= 0) {
            isValid = false;
            tempErrors.fuelConsumption = "Fuel consumption must be entered and greater than 0.";
        } else if (Number(fuelConsumption) > 50) {
            tempErrors.fuelConsumption = "Fuel consumption cannot be greater than 50L/100km.";
            isValid = false;
        }

        if (mileage === "" || mileage === null) {
            isValid = false;
            tempErrors.mileage = "Mileage must be entered.";
        } else if (Number(mileage) < 0) {
            isValid = false;
            tempErrors.mileage = "Mileage must be 0 or greater.";
        } else if (Number(mileage) > 1500000) {
            isValid = false;
            tempErrors.mileage = "Mileage cannot be greater than 1,500,000 km.";
        }

        if (!year) {
            isValid = false;
            tempErrors.year = "Please select a production year.";
        } else if (Number(year) < 1885 || Number(year) > currentYear + 1) {
            isValid = false;
            tempErrors.year = `Production year must be between 1885 and ${currentYear + 1}.`;
        }

        if (!price || Number(price) <= 0) {
            isValid = false;
            tempErrors.price = "Vehicle price must be greater than 0.";
        }

        if (!selectedModel || Number(selectedModel) <= 0) {
            isValid = false;
            tempErrors.model = "Vehicle model must be selected.";
        }

        if (!selectedBrand || Number(selectedBrand) <= 0) {
            isValid = false;
            tempErrors.brand = "Vehicle brand must be selected.";
        }

        setErrors(tempErrors);
        return isValid;
    };
    return (
        <div className="create-offer-wrapper">
            {token ? <LogedNavBar /> : <NotLogedNavbar />}

            <div className="create-div">
    <div className="form-group">
        <select name="brand-select" value={selectedBrand} onChange={(e) => setSelectedBrand(e.target.value)}>
            <option value={""}>Brand</option>
            {brands.map((brand, index) => (
                <option key={index} value={brand.brandName}>{brand.brandName}</option>
            ))}
        </select>
        {errors.brand && <span className="error-message">{errors.brand}</span>}
    </div>

    <div className="form-group">
        <select name='model-select' value={selectedModel} disabled={selectedBrand === ""} onChange={(e) => setSelectedModel(e.target.value)}>
            <option value={""}>Model</option>
            {models.map((model, index) => (
                <option key={index} value={model.modelId}>{model.modelName}</option>
            ))}
        </select>
        {errors.model && <span className="error-message">{errors.model}</span>}
    </div>

    <div className="form-group">
        <select name='bodyType-select' value={selectedBodyType} onChange={(e) => setSelectedBodyType(e.target.value)}>
            <option value={""}>Body Type</option>
            {bodyType.map((bt, index) => (
                <option key={index} value={bt}>{bt}</option>
            ))}
        </select>
        {errors.bodyType && <span className="error-message">{errors.bodyType}</span>}
    </div>

    <div className="form-group">
        <select name='engineType-select' value={selectedEngineType} onChange={(e) => setSelectedEngineType(e.target.value)}>
            <option value={""}>Engine Type</option>
            {engineType.map((et, index) => (
                <option key={index} value={et}>{et}</option>
            ))}
        </select>
        {errors.engineType && <span className="error-message">{errors.engineType}</span>}
    </div>

    <div className="form-group">
        <select name='transmission-select' value={selectedTransmission} onChange={(e) => setSelectedTransmission(e.target.value)}>
            <option value={""}>Transmission</option>
            {transmissions.map((t, index) => (
                <option key={index} value={t}>{t}</option>
            ))}
        </select>
        {errors.transmission && <span className="error-message">{errors.transmission}</span>}
    </div>

    <div className="form-group">
        <input type="number" placeholder='Price' min={0} onChange={(e) => setPrice(e.target.value)} />
        {errors.price && <span className="error-message">{errors.price}</span>}
    </div>

    <div className="form-group">
        <select name='min-year-select' value={year} onChange={(e) => setYear(e.target.value)}>
            <option value="" disabled hidden>Production Year</option>
            {years.map((y, index) => (
                <option key={index} value={y}>{y}</option>
            ))}
        </select>
        {errors.year && <span className="error-message">{errors.year}</span>}
    </div>

    <div className="form-group">
        <input type="text" placeholder='Mileage' onChange={(e) => setMileage(e.target.value)} />
        {errors.mileage && <span className="error-message">{errors.mileage}</span>}
    </div>

    <div className="form-group">
        <input type="number" min={0} placeholder='Fuel Consumption' onChange={(e) => setFuelConsumption(e.target.value)} />
        {errors.fuelConsumption && <span className="error-message">{errors.fuelConsumption}</span>}
    </div>

    <div className="form-group file-group">
        <label htmlFor="file-upload" className="custom-file-upload">
            <span>📁 Pick images</span>
        </label>
        <input 
            id="file-upload"
            type="file" 
            multiple 
            onChange={(e) => setImages(e.target.files)} 
        />
        {images.length > 0 && <span className="file-count">{images.length} fajlova odabrano</span>}
    </div>

    <div className="form-group details-group">
        <textarea 
            placeholder='Add car details' 
            onChange={(e) => setDetails(e.target.value)}
            rows="4"
        />
    </div>

    <button className='create-btn' onClick={() => handleOfferCreation()}>Create Offer</button>
</div>

            {myOffers.length === 0 ? (
                <div className="no-offers-created-div">No Offers Posted</div>
            ) : (
                <div className="my-offers-div">
                    {myOffers.map((o, index) => <OfferCard key={index} offer={o} />)}
                </div>
            )}
            <Footer />
        </div>
    );
}

export default CreateOffer