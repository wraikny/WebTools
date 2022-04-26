module FablePlayground.Utils

let inline lift (a: ^a) : ^b =
  ((^a or ^b): (static member Lift: _ -> _) a)

let inline get (a: ^a) : ^b = (^a: (static member Get: _ -> _) a)

let inline set (v: ^b) (a: ^a) : ^a =
  (^a: (static member Set: _ * _ -> _) a, v)

let getRootUrl () =
  let location = Browser.Dom.window.location
  sprintf "%s//%s" location.protocol location.host
