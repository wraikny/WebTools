module FablePlayground.Pages.About.View

open Fable.Core

open Fable.React
open Fable.React.Props

open FSharp.Data.LiteralProviders

let root =

  div
    [ ClassName "content" ]
    [ h1 [] [ str "About" ]
      p [] [ str "Fableを使って適当なものを置く場所です。" ]

      let path, message =
        Env<"COMMIT_ID">.Value
        |> function
          | "" -> ("commits", "commits")
          | commitId -> (sprintf "commit/%s" commitId, sprintf "commit %s" commitId)

      a
        [ Href(sprintf "https://github.com/wraikny/FablePlayground/%s" path)
          Target "_blank"
          Rel "noopener noreferrer" ]
        [ str (sprintf "%s" message) ] ]
