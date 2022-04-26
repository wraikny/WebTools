module FablePlayground.Pages.SortCharacters.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

open FablePlayground.Pages.SortCharacters

let root model dispatch =
  div
    []
    [ p
        [ ClassName "control" ]
        [ input
            [ ClassName "input"
              Type "text"
              Placeholder "Type"
              (if model.isUpdated then
                 DefaultValue model.input
               else
                 Value model.input)
              AutoFocus true
              OnChange(fun ev -> !!ev.target?value |> ChangeStr |> dispatch) ] ]
      br []
      span [] [ str model.sorted ] ]
