import React from 'react'
import { Navigate, Outlet } from 'react-router-dom'

const PrivateRoutes = () => {

    var token = localStorage.getItem('token')

    return (

        token != null ? <Outlet /> : <Navigate to={'/login'} />

    )
}

export default PrivateRoutes