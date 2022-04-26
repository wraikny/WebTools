module FablePlayground.Utils

let inline lift (a: ^a) : ^b =
  ((^a or ^b): (static member Lift: _ -> _) a)

let inline get (a: ^a) : ^b = (^a: (static member Get: _ -> _) a)

let inline set (v: ^b) (a: ^a) : ^a =
  (^a: (static member Set: _ * _ -> _) a, v)

let getUrlWithPath () =
  let location = Browser.Dom.window.location

  let pathname =
    location.pathname
    |> function
      | "/" -> ""
      | s -> sprintf "/%s" s

  sprintf "%s//%s%s" location.protocol location.host pathname

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

let contentFrame children =
  div [ ClassName "card" ] [ div [ ClassName "card-content" ] [ div [ ClassName "content" ] children ] ]
