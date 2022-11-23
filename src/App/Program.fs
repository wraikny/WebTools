module WebTools.App.Program

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Fable.Core.JsInterop

open Elmish.React
open Elmish.Debug
open Elmish.HMR

open WebTools.Global
open WebTools.App

do
  importAll "../../sass/main.sass"

  Program.mkProgram Model.init Model.update View.root
  |> Program.toNavigable (parseHash PageParser.pageParser) Model.urlUpdate
#if DEBUG
  |> Program.withDebugger
#endif
  |> Program.withReactBatched "elmish-app"
  |> Program.run
