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
  div
    []
    [ yield
        p
          [ ClassName "control" ]
          [ input
              [ ClassName "input"
                Type "text"
                Placeholder "なにか入力してね"
                DefaultValue model.input
                AutoFocus true
                OnChange(fun ev -> !!ev.target?value |> ChangeStr |> dispatch) ] ]
      yield br []

      let text = sprintf "「%s」をソートすると「%s」" model.input model.sorted

      yield p [] [ str text ]
      yield br []

      match Browser.Navigator.navigator.clipboard with
      | None -> ()
      | Some clipboard ->
        yield
          button
            [ ClassName "button"
              OnClick(fun _ev ->
                let root = Utils.getRootUrl ()
                let path = Page.toPath (SortCharacters model.input)
                let _promise = clipboard.writeText (sprintf "%s \n%s/%s" text root path)
                ()
              ) ]
            [ str "結果をコピー" ] ]
