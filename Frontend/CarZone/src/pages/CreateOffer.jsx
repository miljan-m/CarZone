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


    return (
        <div className="create-offer-wrapper">
            {token ? <LogedNavBar /> : <NotLogedNavbar />}
            <div className="create-div">
                <select name="brand-select" value={selectedBrand} onChange={(e) => setSelectedBrand(e.target.value)}>
                    <option value={""}>Brand</option>
                    {

                        brands.map((brand, index) => (
                            <option key={index} value={brand.brandName}>{brand.brandName}</option>
                        ))

                    }
                </select>
                <select name='model-select' value={selectedModel} disabled={selectedBrand == ""} onChange={(e) => setSelectedModel(e.target.value)}>
                    <option value={""}>Model</option>
                    {
                        models.map((model, index) => (
                            <option key={index} value={model.modelId}>{model.modelName}</option>
                        ))
                    }
                </select>

                <select name='bodyType-select' value={selectedBodyType} onChange={(e) => setSelectedBodyType(e.target.value)}>
                    <option value={""}>Body Type</option>
                    {
                        bodyType.map((bodyType, index) => (
                            <option key={index} value={bodyType}>{bodyType}</option>
                        ))
                    }
                </select>

                <select name='engineType-select' value={selectedEngineType} onChange={(e) => setSelectedEngineType(e.target.value)}>
                    <option value={""}>Engine Type</option>
                    {
                        engineType.map((engineType, index) => (
                            <option key={index} value={engineType}>{engineType}</option>
                        ))
                    }
                </select>

                <select name='transmission-select' value={selectedTransmission} onChange={(e) => setSelectedTransmission(e.target.value)}>
                    <option value={""}>Transmission</option>
                    {
                        transmissions.map((transmissions, index) => (
                            <option key={index} value={transmissions}>{transmissions}</option>
                        ))
                    }
                </select>
                <div className='price-range-div'>

                    <input type="number" placeholder='Price' min={0} onChange={(e) => setPrice(e.target.value)} />

                </div>
                <div className='production-year-div' >

                    <select name='min-year-select' value={year} onChange={(e) => setYear(e.target.value)}>
                        <option value="" disabled hidden>Production Year</option>
                        {
                            years.map((year, index) => (
                                <option key={index} value={year}>{year}</option>
                            ))
                        }
                    </select>


                </div>

                <div className='mileage-div' >
                    <input type="text" placeholder='Mileage' onChange={(e) => setMileage(e.target.value)} />
                </div>
                <div className='fuel-consumption-div' >

                    <input type="number" min={0} placeholder='Fuel Consumption' onChange={(e) => setFuelConsumption(e.target.value)} />

                </div>
                <div className="image-import-div">
                    <input type="file" multiple onChange={(e) => setImages(e.target.files)} />
                </div>
                <div className="add-details-div">
                    <input type="text" placeholder='Add your car details' onChange={(e) => setDetails(e.target.value)} />
                </div>
                <button className='button' onClick={() => handleOfferCreation()}>Create</button>
            </div>

            {
                myOffers.length == 0 ? <div className='no-offers-created-div'>No Offers Posted</div> :
                    <div className="my-offers-div">
                        {
                            offers.filter(o => o.user.email == logedUser.email).map((o, index) => (<OfferCard key={index} offer={o} />))
                        }
                    </div>
            }


            <Footer />
        </div>
    )
}

export default CreateOffer