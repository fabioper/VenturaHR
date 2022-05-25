import Cookies from "js-cookie"
import { TokenResponse } from "../dtos/auth/TokenResponse"
import usersApi from "../config/api/users"

const api = usersApi
const endpoint = "/users"

const accessTokenKey = "@venturahr/accessToken"
const refreshTokenKey = "@venturahr/refreshToken"

export function getAccessToken() {
  return Cookies.get(accessTokenKey)
}

export function getRefreshToken() {
  return Cookies.get(refreshTokenKey)
}

export function hasAccessToken() {
  return !!getAccessToken()
}

export const refreshToken = async () => {
  const { data } = await api.post(endpoint + "/refresh", {
    refreshToken: getRefreshToken(),
  })
  saveToken(data)
}

export function saveToken(data: TokenResponse) {
  const options: Cookies.CookieAttributes = {
    sameSite: "strict",
    secure: true,
  }

  Cookies.set(accessTokenKey, data.accessToken, options)
  Cookies.set(refreshTokenKey, data.refreshToken, options)
}

export function removeToken() {
  Cookies.remove(accessTokenKey)
  Cookies.remove(refreshTokenKey)
}
