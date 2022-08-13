namespace FablePlayground

open Elmish

open Browser.Dom

[<RequireQualifiedAccess>]
module Sub =
  let setTitle (value: string) : Sub<'Msg> = fun _ -> document.title <- value
