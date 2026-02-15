import React, { useEffect, useState } from 'react'
import Footer from '../components/Footer'
import axios from 'axios'
import "../styles/Offer.css"
import OfferCard from '../components/OfferCard'
import { useNavigate } from 'react-router-dom'
import LogedNavBar from '../components/LogedNavbar'
import NotLogedNavbar from '../components/NotLogedNavbar'

const Offer = () => {

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
    const [minPrice, setMinPrice] = useState(0)
    const [maxPrice, setMaxPrice] = useState(0)
    const [selectedMinYear, setSelectedMinYear] = useState("")
    const [selectedMaxYear, setSelectedMaxYear] = useState("")
    const [selectedMinMileage, setSelectedMinMileage] = useState("")
    const [selectedMaxMileage, setSelectedMaxMileage] = useState("")
    const [selectedMinFuelConsumption, setSelectedMinFuelConsumption] = useState("")
    const [selectedMaxFuelConsumption, setSelectedMaxFuelConsumption] = useState("")
    const [offers, setOffers] = useState([])
    const token=localStorage.getItem('token')

    const minYear = 1900
    const maxYear = new Date().getFullYear();
    const years = [];

    const mileages = []

    for (let i = maxYear; i >= minYear; i--) {
        years.push(i)
    }

    for (let i = 250000; i >= 1000; i -= 25000) {
        mileages.push(i)
    }


    useEffect(() => {
        axios.get("http://localhost:5047/brands").then(function (response) {
            setBrands(response.data)
            setSelectedBrand(`${response.data[0].brandName}`)
        }).catch(function (error) {
            console.log(error)
        })
    }, [])

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


    //offers
    const handleOfferFetching = () => {

        axios.get('http://localhost:5047/listings').then((response) => {
            setOffers(response.data)
        }).catch(function (error) {
            console.log(error)
        })
    }

    useEffect(() => {
        handleOfferFetching()
    }, [])
    return (
        <div className='offer-wrapper'>
            {token ? <LogedNavBar/> : <NotLogedNavbar/>}
            <div className="search-div">
                <select name="brand-select" value={selectedBrand} onChange={(e) => setSelectedBrand(e.target.value)}>
                    {

                        brands.map((brand, index) => (
                            <option key={index} value={brand.brandName}>{brand.brandName}</option>
                        ))

                    }
                </select>
                <select name='model-select' value={selectedModel} onChange={(e) => setSelectedModel(e.target.value)}>
                    {
                        models.map((model, index) => (
                            <option key={index} value={model.modelName}>{model.modelName}</option>
                        ))
                    }
                </select>

                <select name='bodyType-select' onChange={(e) => setSelectedBodyType(e.target.value)}>
                    {
                        bodyType.map((bodyType, index) => (
                            <option key={index} value={bodyType}>{bodyType}</option>
                        ))
                    }
                </select>

                <select name='engineType-select' onChange={(e) => setSelectedEngineType(e.target.value)}>
                    {
                        engineType.map((engineType, index) => (
                            <option key={index} value={engineType}>{engineType}</option>
                        ))
                    }
                </select>

                <select name='transmission-select' onChange={(e) => setSelectedTransmission(e.target.value)}>
                    {
                        transmissions.map((transmissions, index) => (
                            <option key={index} value={transmissions}>{transmissions}</option>
                        ))
                    }
                </select>
                <div className='price-range-div'>

                    <input type="number" placeholder='Price from ' min={0} onChange={(e) => setMinPrice(e.target.value)} />
                    <input type="number" placeholder='Price to ' min={0} onChange={(e) => setMaxPrice(e.target.value)} />

                </div>
                <div className='production-year-div' >

                    <select name='min-year-select' value={selectedMinYear} onChange={(e) => setSelectedMinYear(e.target.value)}>
                        <option value="">Year from</option>
                        {
                            years.map((year, index) => (
                                <option key={index} value={year}>{year}</option>
                            ))
                        }
                    </select>

                    <select name='max-year-select' value={selectedMaxYear} onChange={(e) => setSelectedMaxYear(e.target.value)}>
                        <option value="">Year to</option>
                        {
                            years.filter(year => year >= selectedMinYear).map((year, index) => (

                                <option key={index} value={year}>{year}</option>
                            ))
                        }
                    </select>

                </div>

                <div className='mileage-div' >

                    <select name='min-mileage-select' value={selectedMinMileage} onChange={(e) => setSelectedMinMileage(e.target.value)}>
                        <option value={""} disabled hidden>Min Mileage</option>

                        {
                            mileages.map((mileage, index) => (

                                <option key={index} value={mileage}>{mileage}</option>
                            ))
                        }

                    </select>

                    <select name='max-mileage-select' value={selectedMaxMileage} onChange={(e) => setSelectedMaxMileage(e.target.value)}>
                        <option value="" disabled hidden>Max Mileage</option>

                        {
                            mileages.filter(mileage => mileage >= selectedMinMileage).map((mileage, index) => (

                                <option key={index} value={mileage}>{mileage}</option>
                            ))
                        }
                    </select>

                </div>
                <div className='fuel-consumption-div' >

                    <input type="number" min={0} placeholder='Min Fuel Consumption' onChange={(e) => setSelectedMinFuelConsumption(e.target.value)} />
                    <input type="number" min={0} placeholder='Max Fuel Consumption' onChange={(e) => setSelectedMaxFuelConsumption(e.target.value)} />

                </div>

                <button className='button'>Search</button>
            </div>
            <div className="my-offers-div">
                {
                    offers.map((o, index) => (<OfferCard key={index} offer={o}  />))
                }
            </div>
            <Footer />
        </div>
    )
}

export default Offer