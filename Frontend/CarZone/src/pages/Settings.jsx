import React, { useEffect, useState } from 'react'
import LogedNavBar from '../components/LogedNavbar'
import Footer from '../components/Footer'
import '../styles/Settings.css'
import axios from 'axios'
import { Link } from 'react-router-dom'
import { CarSimpleIcon, SlidersHorizontalIcon} from "@phosphor-icons/react"
import SettingsCard from '../components/SettingsCard'

const Settings = () => {

  const [brands, setBrands] = useState([])
  const [models, setModels] = useState([])
  const [clickedIcon, setClickedIcon] = useState("brands")

  const addBrandToState = (newBrand) => {
    setBrands(prev => [...prev, newBrand])
  }

  const addModelToState = (newModel) => {
    setModels(prev => [...prev, newModel])
  }

  const removeBrandFromState = (brandId) => {
    setBrands(prev => prev.filter(b => b.brandId !== brandId))
  }

  const removeModelFromState = (modelId) => {
    setModels(prev => prev.filter(m => m.modelId !== modelId))
  }

  //brands
  useEffect(() => {
    axios.get("http://localhost:5047/brands").then(function (response) {
      setBrands(response.data)
    }).catch(function (error) {
      console.log(error)
    })
  }, [])

  //models 
  useEffect(() => {
    axios.get(`http://localhost:5047/models`).then((response) => {
      setModels(response.data)
      console.log(response.data)
    }).catch(function (error) {
      console.log(error)
    })

  }, [])


  return (
    <div className="settings-wrapper">
      <LogedNavBar />
      <div className="setting-div">
        <div className="settings-header">
          <ul>
            <li onClick={() => setClickedIcon("brands")}><CarSimpleIcon /> Brands</li>
            <li onClick={() => setClickedIcon("models")}><SlidersHorizontalIcon /> Models</li>
          </ul>
        </div>
        <SettingsCard clickedIcon={clickedIcon} brands={brands} models={models} addBrandToState={addBrandToState}
          addModelToState={addModelToState} removeBrandFromState={removeBrandFromState} removeModelFromState={removeModelFromState} />
      </div>
      <Footer />
    </div>
  )
}

export default Settings