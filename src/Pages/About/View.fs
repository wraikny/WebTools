module FablePlayground.Pages.About.View

open Fable.React
open Fable.React.Props

let root =
  div
    [ ClassName "content" ]
    [ h1 [] [ str "About" ]
      p [] [ str "Fableを使って適当なものを置く場所です。" ] ]
