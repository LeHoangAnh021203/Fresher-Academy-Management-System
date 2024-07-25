import { Suspense, lazy } from "react"

import { Route, Routes } from "react-router-dom"

import LoadingFallback from "./components/global/animation/LoadingFallback"
import AuthLayout from "./components/global/templates/AuthLayout"
import RootLayout from "./components/global/templates/RootLayout"
import NotFound from "./pages/Not-Found/not-found"
import CreateSyllabusPage from "./pages/Syllabus/CreateSyllabusPage"
import SyllabusDetail from "./pages/Syllabus/syllabus-detail"
import SyllabusList from "./pages/Syllabus/syllabus-list"
import TrainingCalendarPage from "./pages/Training-Calendar/TrainingCalendar"
import UserDetailPage from "./pages/User/UserDetail"

function App() {
  const HomePage = lazy(() => import("./pages/Home/HomePage"))
  const UserListPage = lazy(() => import("./pages/User/UserListPage"))
  const UserPermissionPage = lazy(
    () => import("./pages/User/UserPermissionPage")
  )
  const TrainingProgramList = lazy(
    () => import("./pages/Training-Program/training-program-list")
  )
  const TrainingProgramNew = lazy(
    () => import("./pages/Training-Program/training-program-new")
  )
  const TrainingProgramDetail = lazy(
    () => import("./pages/Training-Program/training-program-detail")
  )
  const ClassListPage = lazy(() => import("./pages/Class/ClassListPage"))
  const ClassDetailPage = lazy(() => import("./pages/Class/ClassDetailPage"))
  const CreateClassPage = lazy(() => import("./pages/Class/ClassCreatePage"))
  const LoginPage = lazy(() => import("./pages/Login/LoginPage"))

  return (
    <Routes>
      {/* public routes */}
      <Route element={<RootLayout />}>
        <Route
          path="/"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <HomePage />
            </Suspense>
          }
        />
        <Route
          path="/users"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <UserListPage />
            </Suspense>
          }
        />
        <Route
          path="/users/:id"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <UserDetailPage />
            </Suspense>
          }
        />
        <Route
          path="/user-permissions"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <UserPermissionPage />
            </Suspense>
          }
        />
        <Route
          path="/training-programs"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <TrainingProgramList />
            </Suspense>
          }
        />
        <Route
          path="/training-programs/new"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <TrainingProgramNew />
            </Suspense>
          }
        />
        <Route
          path="/training-programs/:id"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <TrainingProgramDetail />
            </Suspense>
          }
        />
        <Route
          path="/classes"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <ClassListPage />
            </Suspense>
          }
        />
        <Route
          path="/class/:id"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <ClassDetailPage />
            </Suspense>
          }
        />
        <Route
          path="/create-class"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <CreateClassPage />
            </Suspense>
          }
        />
        <Route
          path="/training-calendar"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <TrainingCalendarPage />
            </Suspense>
          }
        />

        <Route
          path="/syllabus/new"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <CreateSyllabusPage />
            </Suspense>
          }
        />
        <Route
          path="/syllabus"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <SyllabusList />
            </Suspense>
          }
        />
        <Route
          path="/syllabus/:id"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <SyllabusDetail />
            </Suspense>
          }
        />
        {/* <Route path="/loading" element={<LoadingFallback />} /> */}
      </Route>

      <Route element={<AuthLayout />}>
        <Route
          path="/login"
          element={
            <Suspense fallback={<LoadingFallback />}>
              <LoginPage />
            </Suspense>
          }
        />
      </Route>
      <Route path="/not-found" element={<NotFound />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  )
}

export default App
