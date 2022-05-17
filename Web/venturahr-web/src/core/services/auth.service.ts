import {
  createUserWithEmailAndPassword,
  getAuth,
  GithubAuthProvider,
  GoogleAuthProvider,
  signInWithEmailAndPassword,
  signInWithPopup,
  signOut,
  TwitterAuthProvider,
  updateProfile,
  User,
} from "firebase/auth"
import { firebaseApp } from "../config/firebase/firebase.config"
import { SignUpModel } from "../dtos/SignUpModel"
import { LoginModel } from "../dtos/LoginModel"
import { UserRole } from "../enums/UserRole"
import { getFunctions, httpsCallable } from "firebase/functions"
import { AuthUser } from "../models/AuthUser"

const auth = getAuth(firebaseApp)
auth.useDeviceLanguage()

const availableProviders = {
  twitter: () => new TwitterAuthProvider(),
  github: () => new GithubAuthProvider(),
  google: () => new GoogleAuthProvider(),
}

export type ProviderOptions = keyof typeof availableProviders

export async function getToken() {
  const idTokenResult = await auth.currentUser?.getIdTokenResult()
  return idTokenResult?.token
}

export async function signUpUser<T extends SignUpModel>(model: T) {
  const { user } = await createUserWithEmailAndPassword(
    auth,
    model.email,
    model.password
  )

  await finishUserProfile(user.uid, model.role)
  await updateProfile(user, { displayName: model.name })
}

export async function signInUser(credentials: LoginModel) {
  await signInWithEmailAndPassword(
    auth,
    credentials.email,
    credentials.password
  )
}

export function getCurrentUser() {
  const user = auth.currentUser
  return user ? formatUser(user) : null
}

export function onAuthChange(callback: () => Promise<void>) {
  return auth.onAuthStateChanged(async () => {
    const currentUser = getCurrentUser()
    const user = currentUser ? await currentUser : undefined
    const roleIsSet = user?.roles && user?.roles.length > 0

    if (roleIsSet) {
      return callback()
    }
  })
}

interface SignInWithProviderParams {
  providerId: ProviderOptions
  role: UserRole
}

export async function signInWithProvider(params: SignInWithProviderParams) {
  const selectedProvider = {
    twitter: () => new TwitterAuthProvider(),
    github: () => new GithubAuthProvider(),
    google: () => new GoogleAuthProvider(),
  }[params.providerId]
  const { user } = await signInWithPopup(auth, selectedProvider())
  isNewUser(user) && (await finishUserProfile(user.uid, params.role))
}

export async function signOutUser() {
  return await signOut(auth)
}

async function setUserRoles(id: string, role: UserRole): Promise<void> {
  const functions = getFunctions(firebaseApp)
  const assignRoleToUser = httpsCallable(functions, "assignRoleToUser")
  await assignRoleToUser({ id, role })
}

async function finishUserProfile(id: string, role: UserRole) {
  await setUserRoles(id, role)
}

const isNewUser = (user: User): boolean =>
  user.metadata.creationTime === user.metadata.lastSignInTime

const formatUser = async (user: User): Promise<AuthUser> => {
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
