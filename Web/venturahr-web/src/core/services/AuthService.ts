import { UserProfile } from "../models/UserProfile"
import { SignUpModel } from "../dtos/auth/SignUpModel"
import { LoginModel } from "../dtos/auth/LoginModel"
import { TokenResponse } from "../dtos/auth/TokenResponse"
import {
  hasAccessToken,
  refreshToken,
  removeToken,
  saveToken,
} from "./TokenService"
import usersApi from "../config/api/users"

const endpoint = "/users"
const api = usersApi

const authListeners = new Set<() => any>()

export function onAuthChange(callback: (user?: UserProfile) => any) {
  authListeners.add(callback)
  callback()
  return clearAuthListeners
}

export function clearAuthListeners() {
  authListeners.clear()
}

export async function notifyAuthListeners() {
  authListeners.forEach(listener => listener())
}

export async function createUser(model: SignUpModel) {
  await api.post(endpoint, model)
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

export const refresh = async () => {
  await refreshToken()
  await notifyAuthListeners()
}

export async function getCurrentUser() {
  if (isAuthenticated()) {
    const { data } = await api.get<UserProfile>(endpoint + "/profile")
    return new UserProfile({ ...data })
  }
}

export function isAuthenticated() {
  return hasAccessToken()
}
