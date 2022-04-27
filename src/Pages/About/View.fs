module FablePlayground.Pages.About.View

open Fable.Core

open Fable.React
open Fable.React.Props

open FSharp.Data.LiteralProviders

open FablePlayground
open FablePlayground.Global

let root =

  Utils.contentFrame
    [ h1 [] [ str Title.About ]
      div
        [ ClassName "block" ]
        [ p [] [ str "Fableで適当なものを置く場所です。" ]

          match Env<"COMMIT_ID">.Value with
          | "" -> ()
          | commitId ->
            let path, message = (sprintf "commit/%s" commitId, sprintf "commit %s" commitId)

            a
              [ Href(sprintf "https://github.com/wraikny/FablePlayground/%s" path)
                Target "_blank"
                Rel "noopener noreferrer" ]
              [ str (sprintf "%s" message) ] ] ]
