import React, { useRef } from "react"

import ReactQuill from "react-quill"
import "react-quill/dist/quill.snow.css"

interface IProps {
  field?: any
  title?: string
}

const Quill: React.FC<IProps> = ({ field, title }) => {
  const quillRef = useRef<ReactQuill>(null)
  const icons = ReactQuill.Quill.import("ui/icons")
  icons["undo"] =
    `<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-undo-2"><path d="M9 14 4 9l5-5"/><path d="M4 9h10.5a5.5 5.5 0 0 1 5.5 5.5v0a5.5 5.5 0 0 1-5.5 5.5H11"/></svg>`
  icons["redo"] =
    `<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-redo-2"><path d="m15 14 5-5-5-5"/><path d="M20 9H9.5A5.5 5.5 0 0 0 4 14.5v0A5.5 5.5 0 0 0 9.5 20H13"/></svg>`
  const modules = { toolbar: { container } }

  return (
    <div>
      {title && (
        <div className="flex h-[36px] w-full items-center justify-center rounded-t-[15px] bg-primary text-center font-medium text-white">
          {title}
        </div>
      )}
      <ReactQuill
        modules={modules}
        placeholder="Write somethings..."
        {...field}
        ref={quillRef}
        className={`${!title ? "rounded-lg" : "rounded-b-lg"}  min-h-40 border border-primary`}
      />
      <style>{customStyles}</style>
    </div>
  )
}

const container = [
  ["undo", "redo"],
  [{ align: ["", "center", "right", "justify"] }],
  [{ size: ["small", false, "large", "huge"] }],
  [{ color: [] }],
  // <>
  ["clean"],
  [{ list: "bullet" }, { list: "ordered" }],
  ["bold", "italic", "underline", "strike"], // toggled buttons
  ["link", "image"],
  ["code-block", "blockquote"]
]

const customStyles = `
  .ql-container.ql-snow {
    border: none;
  }
  .ql-toolbar.ql-snow {
    border: none;
    border-bottom: 1px solid hsl(var(--primary));
    margin: 0 10px;
  }
`

export default Quill
