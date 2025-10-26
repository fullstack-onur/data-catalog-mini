import { createBrowserRouter, redirect } from 'react-router-dom'
import LoginPage from '@/pages/LoginPage'
import DashboardPage from '@/pages/DashboardPage'
import AssetDetailsPage from '@/pages/AssetDetailsPage'
import AdminPage from '@/pages/AdminPage'


const getAuthStatus = () => {
  const token = localStorage.getItem('auth_token')
  const expiresAt = localStorage.getItem('expires_at')

  if (!token || !expiresAt) return false

  const isExpired = Date.now() > Number(expiresAt)
  return !isExpired
}


export const loginLoader = () => {
  const isLoggedIn = getAuthStatus()
  if (isLoggedIn) {
    throw redirect('/')
  }
  return null
}


export const protectedLoader = () => {
  const isLoggedIn = getAuthStatus()
  if (!isLoggedIn) {
    throw redirect('/login')
  }
  return null
}


export const router = createBrowserRouter([
  {
    path: '/',
    loader: protectedLoader,
    element: <DashboardPage />,
  },
  {
    path: '/assets/:id',
    loader: protectedLoader,
    element: <AssetDetailsPage />,
  },
  {
    path: '/admin',
    loader: protectedLoader,
    element: <AdminPage />,
  },
  {
    path: '/login',
    loader: loginLoader,
    element: <LoginPage />,
  },
  {
    path: '*',
    loader: () => redirect('/'),
  },
])
