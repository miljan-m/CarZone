import React from 'react'
import { Route, Routes } from 'react-router-dom'
import Register from './pages/Register'
import Login from './pages/Login'
import Offer from './pages/Offer'
import CreateOffer from './pages/CreateOffer'
import OfferDetails from './pages/OfferDetails'
import { Account } from './pages/Account'


const App = () => {
  return (
    <Routes>
      <Route path='/' element={<Login />} />
      <Route path='/login' element={<Login />} />
      <Route path='/register' element={<Register />} />
      <Route path='/offers' element={<Offer />} />
      <Route path='/create-offer' element={<CreateOffer />} />
      <Route path='/offer-details' element={<OfferDetails />} />
      <Route path='/account' element={<Account/>} />
    </Routes>
  )
}

export default App