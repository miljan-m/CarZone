import React, { useState } from 'react'
import axios from 'axios';
import { AuthContext } from './AuthContext';
import { useNavigate } from 'react-router-dom';



const AuthProvider = ({ children }) => {
    const [user, setUser] = useState()
    const [token, setToken] = useState(null);
    const navigate = useNavigate();

    const handleLogin = async (email, password) => {
        await axios.post("http://localhost:5047/user/login", {
            email,
            password
        }).then(function (response) {
            setToken(response.data.token)
            localStorage.setItem("token", response.data.token);
            localStorage.setItem("user", JSON.stringify(response.data))
            setUser(response.data)
            navigate('/offers')
            console.log(response.data)
        }).catch(function (error) {
            console.log(error)
        })
    }

    const handleLogout = () => {
        setUser(null)
        setToken(null)
        localStorage.removeItem('token')
        localStorage.removeItem('user')
        console.log("asd")
        navigate('/login')
    }

    return (
        <AuthContext.Provider value={{ user, token, handleLogin, handleLogout }}>
            {children}
        </AuthContext.Provider>
    )
}

export default AuthProvider