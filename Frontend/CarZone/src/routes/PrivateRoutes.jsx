import React, { useContext } from 'react'
import { Navigate, Outlet } from 'react-router-dom'
import { AuthContext } from '../Authentication/AuthContext'

const PrivateRoutes = ({ roles }) => {

    var token = localStorage.getItem('token')
    const user = JSON.parse(localStorage.getItem('user'))


    if (!token) return <Navigate to={'/login'} />
    if (!user || !user.roles.some(r => roles.includes(r))) return <div>Unauthorised</div>
    return <Outlet />
}

export default PrivateRoutes