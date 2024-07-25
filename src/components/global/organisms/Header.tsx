import { useAuthContext } from "@/contexts/auth-provider"
import { Link } from "react-router-dom"

import { Avatar, AvatarFallback, AvatarImage } from "../atoms/avatar"
import { Button } from "../atoms/button"
import { MobileSidebar } from "./MobileSidebar"
import fpt_logo from "/Fpt_Logo.svg"
import unigate_logo from "/Unigate_logo.svg"
import avatar from "/avatars/user2.png"

const Header = () => {
  const { logout, user } = useAuthContext()
  return (
    <div className="flex h-[60px] w-full items-center justify-between border-b border-gray-200 bg-primary px-10 shadow-sm">
      <div className="flex items-center">
        <Link to="#" className="hidden lg:block">
          <img className="w-[74px]" src={fpt_logo} alt="FPT Logo" />
        </Link>
        <div className="block lg:hidden">
          <MobileSidebar />
        </div>
      </div>
      <div className="flex items-center gap-10">
        <div className="hidden cursor-pointer items-start justify-start gap-2 rounded-3xl bg-slate-900 px-3 py-1 sm:flex">
          <div className="inline-flex items-center justify-start gap-1">
            <img className="h-5 w-5" src={unigate_logo} alt="Unigate Logo" />
            <div className="text-xs text-white">uniGate</div>
          </div>
        </div>
        <div className="flex h-10 gap-2">
          <Avatar>
            <AvatarImage src={avatar} />
            <AvatarFallback>UN</AvatarFallback>
          </Avatar>
          <div className="flex flex-col items-start">
            <p className="text-sm font-semibold leading-normal text-white">
              {user?.UserName}
            </p>
            <Button
              size="sm"
              variant="link"
              className="p-0 text-sm font-light leading-normal text-white"
              onClick={() => logout()}
            >
              Log out
            </Button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Header
