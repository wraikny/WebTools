module WebTools.Pages.SortCharacters.View

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

open WebTools
open WebTools.Global
open WebTools.Pages.SortCharacters

let private resultView model =
  div
    []
    [ Utils.View.messageBox "結果" [ str model.sorted ]
      yield!
        Utils.View.copyToClipboard
          (model.input.Length = 0)
          (sprintf
            (if model.input = model.sorted then
               "「%s」をソートしても「%s」"
             else
               "「%s」をソートすると「%s」")
            model.input
            model.sorted) ]

let root model dispatch =
  if model.initializedFromQuery then
    dispatch Msg.Initialized

  Utils.View.contentFrame
    [ h1 [ ClassName "title" ] [ str Title.SortCharacters ]
      div
        [ ClassName "block" ]
        [ p
            [ ClassName "control has-icons-left" ]
            [ input
                [ ClassName "input is-rounded"
                  Type "text"
                  Placeholder "なにか入力してね"
                  (if model.initializedFromQuery then
                     Value model.input
                   else
                     DefaultValue model.input)
                  AutoFocus true
                  OnChange(fun ev -> !!ev.target?value |> ChangeStr |> dispatch) ]
              span [ ClassName "icon is-small is-left" ] [ i [ ClassName "fa fa-pen" ] [] ] ] ]

      div [ ClassName "block" ] [ resultView model ] ]
