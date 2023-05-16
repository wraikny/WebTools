module WebTools.Utils

let mapFst f (a, b) = (f a, b)

let inline lift (a: ^a) : ^b =
  ((^a or ^b): (static member Lift: _ -> _) a)

let inline get (a: ^a) : ^b =
  ((^a or ^b): (static member Get: ^a * ^b -> ^b) a, Unchecked.defaultof< ^b>)

let inline set (v: ^b) (a: ^a) : ^a =
  ((^a or ^b): (static member Set: _ * _ -> _) a, v)

module View =
  open Fable.React
  open Fable.React.Props

  let contentFrame children =
    div [ ClassName "card" ] [ div [ ClassName "card-content" ] [ div [ ClassName "content" ] children ] ]

  let messageBox title children =
    div
      [ ClassName "message" ]
      [ div [ ClassName "message-header" ] [ p [] [ str title ] ]
        div [ ClassName "message-body overflow-wrap" ] [ div [ ClassName "break-word" ] children ] ]

  let copyToClipboard disabled text =
    match Browser.Navigator.navigator.clipboard with
      | Some clipboard ->
        [ div
            [ ClassName "buttons" ]
            [ button
                [ ClassName "button"
                  Disabled disabled
                  OnClick(fun _ev ->
                    let url = Browser.Dom.window.location.href
                    let _promise = clipboard.writeText (sprintf "%s \n%s" text url)
                    ()
                  ) ]
                [ span [ ClassName "icon" ] [ i [ ClassName "far fa-copy" ] [] ]
                  span [] [ str "コピー" ] ] ]
        ]
      | _ -> List.empty