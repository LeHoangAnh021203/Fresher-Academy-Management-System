// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app"
import { getStorage } from "firebase/storage"

const firebaseConfig = {
  apiKey: "AIzaSyD2xyRrDmYDnYL4tDKc7iz0wU3n7xLxsfk",
  authDomain: "fams-9a138.firebaseapp.com",
  projectId: "fams-9a138",
  storageBucket: "fams-9a138.appspot.com",
  messagingSenderId: "155741291152",
  appId: "1:155741291152:web:a243674f3377714f7a0e15",
  measurementId: "G-EY2G1F9JE5"
}

// Initialize Firebase
const app = initializeApp(firebaseConfig)
export const fileDB = getStorage(app)
