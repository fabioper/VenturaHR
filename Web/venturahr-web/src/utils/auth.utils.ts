import { User } from "firebase/auth"
import { AuthUser } from "../contexts/AuthUser"
import { getFunctions, httpsCallable } from "firebase/functions"
import { firebaseApp } from "../config/firebase/firebase.config"
import { UserRole } from "../core/enums/UserRole"

export const formatUser = async (user: User): Promise<AuthUser> => {
  const token = await user.getIdTokenResult(true)
  return new AuthUser({
    id: user.uid,
    name: user.displayName || "",
    email: user.email || "",
    jwt: token.token,
    roles: (token.claims.role as string[]) || [],
    photoUrl: user.photoURL || undefined,
  })
}

export const isNewUser = (user: User): boolean =>
  user.metadata.creationTime === user.metadata.lastSignInTime

export const setUserRole = async (
  id: string,
  role: UserRole
): Promise<void> => {
  const functions = getFunctions(firebaseApp)
  const assignRoleToUser = httpsCallable(functions, "assignRoleToUser")
  await assignRoleToUser({ id, role })
}