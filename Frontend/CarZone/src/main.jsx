import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App'
import { BrowserRouter } from 'react-router-dom'
import AuthProvider from './Authentication/AuthProvider'
import ChatProvider from './Chat/ChatProvider'



createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <ChatProvider>
        <AuthProvider>
          <App />
        </AuthProvider>
      </ChatProvider>
    </BrowserRouter>
  </StrictMode>,
)
