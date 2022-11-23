module WebTools.Pages.About.View

open Fable.Core

open Fable.React
open Fable.React.Props

open FSharp.Data.LiteralProviders

open WebTools
open WebTools.Global

let private commitId () =
  [ match Env<"COMMIT_ID">.Value with
    | "" -> ()
    | commitId ->
      let path, message = (sprintf "commit/%s" commitId, sprintf "commit %s" commitId)

      a
        [ Href(sprintf "https://github.com/wraikny/WebTools/%s" path)
          Target "_blank"
          Rel "noopener noreferrer" ]
        [ str (sprintf "%s" message) ] ]

let root =

  Utils.contentFrame
    [ h1 [] [ str Title.About ]
      div
        [ ClassName "block" ]
        [ p [] [ str "Fableで適当なものを置く場所です。" ]
          yield! commitId () ] ]
