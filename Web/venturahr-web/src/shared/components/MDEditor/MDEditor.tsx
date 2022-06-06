import React, { useState } from "react"
import { marked } from "marked"
import { InputTextarea } from "primereact/inputtextarea"
import { Dialog } from "primereact/dialog"
import { Button } from "primereact/button"
import { PrimeIcons } from "primereact/api"

interface MDEditorProps {
  id: string
  name: string
  onChange: React.ChangeEventHandler<HTMLTextAreaElement>
  onBlur: React.FocusEventHandler<HTMLTextAreaElement>
  value: string
}

const MDEditor: React.FC<MDEditorProps> = props => {
  const [isPreviewVisible, setIsPreviewVisible] = useState(false)

  return (
    <div>
      <InputTextarea
        id={props.id}
        name={props.name}
        onChange={props.onChange}
        onBlur={props.onBlur}
        value={props.value}
        style={{
          minHeight: "200px",
          fontFamily: "monospace",
          fontSize: ".8rem",
        }}
      />

      <div className="flex justify-between items-center">
        <p className="font-body text-xs text-slate-400">
          Este formulário utiliza o formato{" "}
          <a
            href="https://www.markdownguide.org/basic-syntax/"
            target="_blank"
            rel="noreferrer"
            className="text-purple-400"
          >
            Markdown
          </a>
          .
        </p>

        <Button
          label="Preview"
          icon={PrimeIcons.EYE}
          className="p-button-sm p-button-outlined mt-2"
          disabled={!props.value}
          onClick={() => setIsPreviewVisible(true)}
        />
      </div>

      <Dialog
        onHide={() => setIsPreviewVisible(false)}
        visible={isPreviewVisible}
        style={{ minWidth: "50vw" }}
      >
        <div
          dangerouslySetInnerHTML={{ __html: marked.parse(props.value) }}
          style={{ minHeight: "350px" }}
        ></div>
      </Dialog>
    </div>
  )
}

export default MDEditor
