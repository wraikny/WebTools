module FablePlayground.Pages.SortCharacters.View

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

open FablePlayground
open FablePlayground.Global
open FablePlayground.Pages.SortCharacters

let root model dispatch =
  if model.initializedFromQuery then
    dispatch Msg.Initialized

  div
    []
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
              OnChange(fun ev -> !!ev.target?value |> ChangeStr |> dispatch) ] ]
      br []

      div
        [ ClassName "content" ]
        [ div
            [ ClassName "block" ]
            [

              if model.sorted <> "" then
                yield
                  div
                    [ ClassName "box" ]
                    [ yield p [] [ str model.sorted ]

                      match Browser.Navigator.navigator.clipboard with
                      | Some clipboard ->
                        yield
                          button
                            [ ClassName "button"
                              OnClick(fun _ev ->
                                let root = Utils.getRootUrl ()
                                let path = Page.toPath (SortCharacters model.input)

                                let text = sprintf "「%s」をソートすると「%s」" model.input model.sorted
                                let _promise = clipboard.writeText (sprintf "%s \n%s/%s" text root path)
                                ()
                              ) ]
                            [ str "結果をコピー" ]

                        yield br []
                      | _ -> () ] ]

          div
            [ ClassName "buttons" ]
            [ button
                [ ClassName "button"
                  OnClick(fun _ -> dispatch Memo) ]
                [ str "メモ" ]
              button
                [ ClassName "button"
                  OnClick(fun _ -> dispatch ClearHistory) ]
                [ str "クリア" ] ]

          table
            [ ClassName "table" ]
            [ thead
                []
                [ tr
                    []
                    [ th [] [ str "入力" ]
                      th [] [ str "出力" ] ] ]
              tbody
                []
                (model.history
                 |> List.map (fun (input, sorted) ->
                   tr
                     []
                     [ td [] [ str input ]
                       td [] [ str sorted ] ]
                 )) ] ] ]
