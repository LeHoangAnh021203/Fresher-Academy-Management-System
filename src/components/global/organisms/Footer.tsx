const Footer = () => {
  const year = new Date().getFullYear()
  return (
    <div className="inline-flex h-16 w-full items-center justify-center bg-primary p-2.5">
      <p className="text-sm font-normal text-white sm:text-base">
        {`Copyright © ${year} BA Warrior. All Rights Reserved.`}
      </p>
    </div>
  )
}

export default Footer
