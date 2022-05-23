import api from "../config/api"
import { UserProfile } from "../models/UserProfile"
import { SignUpModel } from "../dtos/auth/SignUpModel"
import { LoginModel } from "../dtos/auth/LoginModel"

const endpoint = "/users"

export async function createUser(model: SignUpModel) {
  await api.post(endpoint, model)
}

interface TokenResponse {
  accessToken: string
  refreshToken: string
}

export async function login(model: LoginModel) {
  const { data } = await api.post<TokenResponse>(endpoint + "/login", model)
  saveToken(data)
  await notifyAuthListeners()
}

export async function logout() {
  removeToken()
  await notifyAuthListeners()
}

export async function getCurrentUser() {
  const { data } = await api.get<UserProfile>(endpoint + "/profile")
  return new UserProfile({ ...data })
}

const accessTokenKey = "@venturahr/accessToken"
const refreshTokenKey = "@venturahr/refreshToken"

const authListeners = new Set<() => any>()

export function onAuthChange(callback: (user?: UserProfile) => any) {
  authListeners.add(callback)
  callback()
  return clearAuthListeners
}

export function clearAuthListeners() {
  authListeners.clear()
}

async function notifyAuthListeners() {
  authListeners.forEach(listener => listener())
}

export function getAccessToken() {
  return localStorage.getItem(accessTokenKey)
}

function getRefreshToken() {
  return localStorage.getItem(refreshTokenKey)
}

export function hasAccessToken() {
  return !!getAccessToken()
}

export const refreshToken = async () => {
  const { data } = await api.post(endpoint + "/refresh", {
    refreshToken: getRefreshToken(),
  })
  saveToken(data)
  await notifyAuthListeners()
}

function saveToken(data: TokenResponse) {
  localStorage.setItem(accessTokenKey, data.accessToken)
  localStorage.setItem(refreshTokenKey, data.refreshToken)
}

function removeToken() {
  localStorage.removeItem(accessTokenKey)
  localStorage.removeItem(refreshTokenKey)
}
