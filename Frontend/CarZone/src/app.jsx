import React from 'react'
import { Route, Routes } from 'react-router-dom'
import Register from './pages/Register'
import Login from './pages/Login'
import Offer from './pages/Offer'
import CreateOffer from './pages/CreateOffer'
import OfferDetails from './pages/OfferDetails'
import { Account } from './pages/Account'
import UpdateAccount from './pages/UpdateAccount'
import PrivateRoutes from './routes/PrivateRoutes'
import Settings from './pages/Settings'


const App = () => {
  return (
    <Routes>
      <Route path='/' element={<Login />} />
      <Route path='/login' element={<Login />} />
      <Route path='/register' element={<Register />} />
      <Route path='/offers' element={<Offer />} />
      <Route path='/offer-details' element={<OfferDetails />} />
      <Route element={<PrivateRoutes roles={['User']} />}>
        <Route path='/create-offer' element={<CreateOffer />} />
        <Route path='/account' element={<Account />} />
        <Route path='/update-account' element={<UpdateAccount />} />
      </Route>
      <Route element={<PrivateRoutes roles={['Admin', 'User']} />}>
        <Route path='/settings' element={<Settings />} />
      </Route>
    </Routes>
  )
}

export default App