module FablePlayground.Pages.SortCharacters.View

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

open FablePlayground
open FablePlayground.Global
open FablePlayground.Pages.SortCharacters

let private resultView model =
  div
    []
    [ div
        [ ClassName "message" ]
        [ div [ ClassName "message-header" ] [ p [] [ str "結果" ] ]
          div [ ClassName "message-body" ] [ str model.sorted ] ]

      match Browser.Navigator.navigator.clipboard with
      | Some clipboard ->
        div
          [ ClassName "buttons" ]
          [ button
              [ ClassName "button"
                Disabled(model.input.Length = 0)
                OnClick(fun _ev ->
                  let url = Browser.Dom.window.location.href

                  let text = sprintf "「%s」をソートすると「%s」" model.input model.sorted
                  let _promise = clipboard.writeText (sprintf "%s \n%s" text url)
                  ()
                ) ]
              [ str "結果をコピー" ] ]
      | _ -> () ]

// let private createHistoryTable history =
//   table
//     [ ClassName "table is-striped is-fullwidth" ]
//     [ thead
//         []
//         [ tr
//             []
//             [ th [] [ str "入力" ]
//               th [] [ str "出力" ] ] ]
//       tbody
//         []
//         (history
//          |> List.map (fun (input, sorted) ->
//            tr
//              []
//              [ td [] [ str input ]
//                td [] [ str sorted ] ]
//          )) ]

let root model dispatch =
  if model.initializedFromQuery then
    dispatch Msg.Initialized

  Utils.contentFrame
    [ h1 [ ClassName "title" ] [ str "文字をソートするやつ" ]
      div
        [ ClassName "block" ]
        [ p
            [ ClassName "control" ]
            [ input
                [ ClassName "input"
                  Type "text"
                  Placeholder "なにか入力してね"
                  (if model.initializedFromQuery then
                     Value model.input
                   else
                     DefaultValue model.input)
                  AutoFocus true
                  OnChange(fun ev -> !!ev.target?value |> ChangeStr |> dispatch) ] ] ]

      div [ ClassName "block" ] [ resultView model ]
      // div
      //   [ ClassName "block" ]
      //   [ div
      //       []
      //       [ h2 [ ClassName "subtitle" ] [ str "履歴" ]
      //         div
      //           [ ClassName "buttons" ]
      //           [ let createButton disabled label msg =
      //               button
      //                 [ Disabled disabled
      //                   ClassName "button"
      //                   OnClick(fun _ -> dispatch msg) ]
      //                 [ str label ]

      //             createButton (model.input = "") "メモ" Memo
      //             createButton false "クリア" ClearHistory ]

      //         createHistoryTable model.history ] ]
      ]
