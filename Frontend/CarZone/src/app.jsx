import React from 'react'
import { Route, Routes } from 'react-router-dom'
import Register from './pages/Register'
import Login from './pages/Login'
import Offer from './pages/Offer'
import CreateOffer from './pages/CreateOffer'


const App = () => {
  return (
    <Routes>
      <Route path='/' element={<Login/>}/>
      <Route path='/login' element={<Login/>}/>
      <Route path='/register' element={<Register/>}/>
      <Route path='/offers' element={<Offer/>}/>
      <Route path='create-offer' element={<CreateOffer/>}/>
    </Routes>
  )
}

export default App