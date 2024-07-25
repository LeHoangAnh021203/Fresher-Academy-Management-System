import { ReactNode, useState } from "react"

import { CREATE_PERMISSION } from "@/constants/user"
import { useAuthContext } from "@/contexts/auth-provider"
import {
  AlignJustify,
  BookOpen,
  CalendarDays,
  ChevronDown,
  GraduationCap,
  Home,
  MicroscopeIcon,
  User,
  X
} from "lucide-react"
import { Link, useLocation, useNavigate } from "react-router-dom"
import { toast } from "sonner"

import { IUserRole } from "@/types/user.interface"

import { cn } from "@/lib/utils"

interface IMenu {
  id: number
  title: string
  link: string
  icon?: ReactNode
  isHasPermission?: boolean
  submenu?: boolean
  submenuItems?: { link: string; title: string; isHasPermission?: boolean }[]
}

const SideBar = () => {
  const location = useLocation()
  const [open, setOpen] = useState(true)
  const [openSubmenus, setOpenSubmenus] = useState<number[]>([])
  const currentUrl = location.pathname

  //!Role check
  const { role, permissions } = useAuthContext()

  const navigation = useNavigate()

  const Menus: IMenu[] = [
    { id: 1, title: "Home", link: "/", icon: <Home size={20} /> },
    {
      id: 2,
      title: "Syllabus",
      link: "#",
      icon: <BookOpen size={20} />,
      isHasPermission: permissions?.syllabus !== 0,
      submenu: true,
      submenuItems: [
        {
          title: "View syllabus",
          link: "/syllabus",
          isHasPermission: true
        },
        {
          title: "Create syllabus",
          link: "/syllabus/new",
          isHasPermission: CREATE_PERMISSION.includes(
            permissions?.syllabus ?? 0
          )
        }
      ]
    },
    {
      id: 3,
      title: "Training program",
      link: "#",
      icon: <MicroscopeIcon size={20} />,
      isHasPermission: permissions?.trainingProgram !== 0,
      submenu: true,
      submenuItems: [
        {
          title: "View program",
          link: "/training-programs",
          isHasPermission: true
        },
        {
          title: "Create program",
          link: "/training-programs/new",
          isHasPermission: CREATE_PERMISSION.includes(
            permissions?.trainingProgram ? permissions?.trainingProgram : 0
          )
        }
      ]
    },
    {
      id: 4,
      title: "Class",
      link: "#",
      icon: <GraduationCap size={20} />,
      isHasPermission: permissions?.class !== 0,
      submenu: true,
      submenuItems: [
        { title: "View class", link: "/classes", isHasPermission: true },
        {
          title: "Create class",
          link: "/create-class",
          isHasPermission: CREATE_PERMISSION.includes(
            permissions?.class ? permissions?.class : 0
          )
        }
      ]
    },
    {
      id: 4,
      title: "Training calendar",
      link: "/training-calendar",
      icon: <CalendarDays size={20} />,
      isHasPermission: true
    },
    {
      id: 6,
      title: "User management",
      link: "#",
      icon: <User size={20} />,
      isHasPermission: permissions?.userManagement !== 0,
      submenu: true,
      submenuItems: [
        { title: "User list", link: "/users", isHasPermission: true },
        {
          title: "User permission",
          link: "/user-permissions",
          isHasPermission: true
        }
      ]
    }
  ]

  const toggleSubmenu = (id: number) => {
    setOpenSubmenus((prevOpenSubmenus) => {
      if (prevOpenSubmenus.includes(id)) {
        return prevOpenSubmenus.filter((submenuId) => submenuId !== id)
      } else {
        return [...prevOpenSubmenus, id]
      }
    })
  }

  const showNoPermissionErr = () => {
    toast.error("Bạn đéo có quyền")
  }

  const handleToggle = () => {
    setOpen(!open)
    setOpenSubmenus([])
  }

  return (
    <div
      className={cn(
        "sticky hidden h-fit min-h-screen max-w-80 bg-slate-100 p-5 duration-300 lg:block",
        open ? "w-80" : "w-20"
      )}
    >
      <button
        onClick={() => handleToggle()}
        className="rounded-md p-2 text-primary duration-300 hover:bg-gray-300/90 hover:text-gray-800"
      >
        {open ? (
          <X className="h-5 w-5" />
        ) : (
          <AlignJustify className="h-5 w-5" />
        )}
      </button>

      <ul className="pt-2">
        {Menus.map((menu, index) => (
          <>
            <Link to={menu.link}>
              <li
                key={index}
                className={cn(
                  "mt-2 flex cursor-pointer items-center gap-x-4 rounded-md p-2 text-sm hover:bg-slate-200",
                  menu.link === currentUrl &&
                    "bg-gray-300 text-gray-800 hover:bg-gray-300/80"
                )}
                onClick={
                  !menu?.isHasPermission
                    ? () => showNoPermissionErr()
                    : menu.submenu
                      ? () => toggleSubmenu(menu.id)
                      : undefined
                }
              >
                <span className="float-left block">{menu.icon}</span>
                <span
                  className={cn(
                    "flex-1 flex-nowrap whitespace-nowrap text-base font-normal duration-300",
                    open ? "" : "hidden"
                  )}
                >
                  {menu.title}
                </span>
                {menu.submenu && open && (
                  <ChevronDown
                    size={20}
                    className={cn(
                      "",
                      openSubmenus.includes(menu.id) && "rotate-90 duration-150"
                    )}
                  />
                )}
              </li>
            </Link>

            {menu.submenu && open && openSubmenus.includes(menu.id) && (
              <ul>
                {menu?.submenuItems?.map((submenuItem, index) => (
                  <li
                    className={cn(
                      "mt-2 flex cursor-pointer items-center justify-between  gap-x-4 rounded-md p-2 px-10 text-right text-sm duration-300 hover:bg-slate-200",
                      submenuItem.link === currentUrl &&
                        "bg-gray-300 text-gray-800 hover:bg-gray-300/80",
                      role !== IUserRole.SuperAdmin &&
                        submenuItem.link === "/user-permissions" &&
                        "hidden"
                    )}
                    key={index}
                    onClick={
                      !submenuItem?.isHasPermission
                        ? () => showNoPermissionErr()
                        : () => navigation(submenuItem.link)
                    }
                  >
                    {submenuItem.title}
                  </li>
                ))}
              </ul>
            )}
          </>
        ))}
      </ul>
    </div>
  )
}

export default SideBar
