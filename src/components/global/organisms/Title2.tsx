type title2Props = {
  title: string
}

function HeaderUser({ title }: title2Props) {
  return (
    <h2 className="mb-[30px] px-5 py-[15px] text-2xl font-semibold leading-9 tracking-[4.8px] text-primary">
      {title}
    </h2>
  )
}

export default HeaderUser
