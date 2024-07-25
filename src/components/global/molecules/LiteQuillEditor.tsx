import React from "react"

import ReactQuill from "react-quill"
import "react-quill/dist/quill.snow.css"

interface IProps {
  field?: any
  title?: string
}

const LiteQuill: React.FC<IProps> = ({ field, title }) => {
  return (
    <div className="w-full">
      {title && (
        <div className="h-[30px] w-full rounded-t-[15px] bg-primary text-center font-medium text-white">
          {title}
        </div>
      )}
      <ReactQuill
        theme="snow"
        modules={modules}
        placeholder="Write something..."
        {...field}
        className="min-h-[140px] w-full rounded-lg border border-primary"
      />
      <style>{customStyles}</style>
    </div>
  )
}

const modules = {
  toolbar: false
}

const customStyles = `
  .ql-container.ql-snow {
    border: none;
  }
`

export default LiteQuill
