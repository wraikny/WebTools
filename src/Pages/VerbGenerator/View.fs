module WebTools.Pages.VerbGenerator.View

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

open WebTools
open WebTools.Global
open WebTools.Pages.VerbGenerator

let resultView model =
  div
    []
    [ Utils.View.messageBox
        "結果"
        [ ul
            []
            (model.words
             |> List.map (fun word -> li [] [ str word ])) ]
      yield!
        Utils.View.copyToClipboard
          (model.words.Length = 0)
          (model.words
           |> List.map (sprintf "「%s」")
           |> String.concat "\n"
           |> sprintf "以下の単語が選ばれました。\n%s") ]

let root model dispatch =
  Utils.View.contentFrame
    [ h1 [ ClassName "title" ] [ str Title.VerbGenerator ]
      div
        [ ClassName "block" ]
        [ p
            [ ClassName "control" ]
            [ div
                [ ClassName "select" ]
                [ select
                    [ Value model.count
                      OnChange(fun ev -> !!ev.target?value |> SetCount |> dispatch) ]
                    [ for i in 1..10 do
                        option [ Value i ] [ str (i.ToString()) ] ] ]
              button
                [ ClassName "button"
                  OnClick(fun _ -> dispatch Generate) ]
                [ str "生成" ] ] ]
      div [ ClassName "block" ] [ resultView model ] ]
