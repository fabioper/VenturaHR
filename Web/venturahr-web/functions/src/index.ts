import * as functions from "firebase-functions"
import * as admin from "firebase-admin"

admin.initializeApp()

export const assignRoleToUser = functions.https.onCall(async data => {
  const { email, role } = data
  const user = await admin.auth().getUserByEmail(email)
  return await admin.auth().setCustomUserClaims(user.uid, { role: [role] })
})
