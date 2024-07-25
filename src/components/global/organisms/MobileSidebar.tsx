import { useState } from "react"

import { Menu } from "lucide-react"
import {
  BookOpen,
  CalendarDays,
  ChevronDown,
  Folder,
  GraduationCap,
  Home,
  MicroscopeIcon,
  Settings,
  User
} from "lucide-react"
import { Link } from "react-router-dom"

import { cn } from "@/lib/utils"

import { Sheet, SheetContent, SheetTrigger } from "../atoms/sheet"
import fpt_logo from "/Fpt_Logo.svg"

export const MobileSidebar = () => {
  const [openSubmenus, setOpenSubmenus] = useState<number[]>([])
  const Menus = [
    { id: 1, title: "Home", link: "/", icon: <Home size={20} /> },
    {
      id: 2,
      title: "Syllabus",
      link: "#",
      icon: <BookOpen size={20} />,
      submenu: true,
      submenuItems: [
        { title: "View syllabus", link: "/syllabus" },
        { title: "Create syllabus", link: "/syllabus/new" }
      ]
    },
    {
      id: 3,
      title: "Training program",
      link: "#",
      icon: <MicroscopeIcon size={20} />,
      submenu: true,
      submenuItems: [
        { title: "View program", link: "/training-programs" },
        { title: "Create program", link: "/training-programs/new" }
      ]
    },
    {
      id: 4,
      title: "Class",
      link: "#",
      icon: <GraduationCap size={20} />,
      submenu: true,
      submenuItems: [
        { title: "View class", link: "/classes" },
        { title: "Create class", link: "/create-class" }
      ]
    },
    {
      id: 4,
      title: "Training calendar",
      link: "/training-calendar",
      icon: <CalendarDays size={20} />
    },
    {
      id: 6,
      title: "User management",
      link: "#",
      icon: <User size={20} />,
      submenu: true,
      submenuItems: [
        { title: "User list", link: "/users" },
        { title: "User permission", link: "/user-permissions" }
      ]
    },
    {
      id: 6,
      title: "Learning material",
      link: "#",
      icon: <Folder size={20} />
    },
    {
      id: 8,
      title: "Setting",
      link: "#",
      icon: <Settings size={20} />,
      submenu: true,
      submenuItems: [{ title: "Calendar", link: "#" }]
    }
  ]
  const currentUrl = location.pathname

  const toggleSubmenu = (id: number) => {
    setOpenSubmenus((prevOpenSubmenus) => {
      if (prevOpenSubmenus.includes(id)) {
        return prevOpenSubmenus.filter((submenuId) => submenuId !== id)
      } else {
        return [...prevOpenSubmenus, id]
      }
    })
  }

  return (
    <Sheet>
      <SheetTrigger>
        <button className="flex items-center justify-center rounded-md border border-gray-600 bg-gray-800 px-3 py-2 text-white hover:border-gray-500 hover:bg-gray-700">
          <Menu className="h-6 w-6" />
        </button>
      </SheetTrigger>
      <SheetContent side="left" className="bg-slate-100 p-0">
        <img className="mx-7 mt-4 w-[80px]" src={fpt_logo} />
        <div className={"inset-y-0 bg-slate-100 p-5 duration-300 "}>
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
                      menu.submenu ? () => toggleSubmenu(menu.id) : undefined
                    }
                  >
                    <span className="float-left block">{menu.icon}</span>
                    <span
                      className={cn(
                        "flex-1 flex-nowrap whitespace-nowrap text-base font-normal duration-300"
                      )}
                    >
                      {menu.title}
                    </span>
                    {menu.submenu && (
                      <ChevronDown
                        size={20}
                        className={cn(
                          "",
                          openSubmenus.includes(menu.id) && "rotate-180"
                        )}
                      />
                    )}
                  </li>
                </Link>

                {menu.submenu && openSubmenus.includes(menu.id) && (
                  <ul>
                    {menu.submenuItems.map((submenuItem, index) => (
                      <Link to={submenuItem.link}>
                        <li
                          className={cn(
                            "mt-2 flex cursor-pointer items-center justify-between gap-x-4 rounded-md p-2 px-5 text-right text-sm hover:bg-slate-200",
                            submenuItem.link === currentUrl &&
                              "bg-gray-300 text-gray-800 hover:bg-gray-300/80"
                          )}
                          key={index}
                        >
                          {submenuItem.title}
                        </li>
                      </Link>
                    ))}
                  </ul>
                )}
              </>
            ))}
          </ul>
        </div>
      </SheetContent>
    </Sheet>
  )
}
