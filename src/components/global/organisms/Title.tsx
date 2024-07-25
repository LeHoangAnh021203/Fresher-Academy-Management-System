type titleProps = {
  title: string
}

function Title({ title }: titleProps) {
  return (
    <div className="mb-[30px] bg-primary p-5 text-2xl font-semibold tracking-[4.8px] text-white">
      {title}
    </div>
  )
}

export default Title
