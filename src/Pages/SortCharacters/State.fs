namespace FablePlayground.Pages.SortCharacters

open Elmish
open Elmish.Navigation

open FablePlayground.Global

type Msg =
  | ChangeStr of string
  | Initialized
// | Memo
// | ClearHistory

type Model =
  { input: string
    sorted: string
    // history: (string * string) list
    initializedFromQuery: bool }

module Model =
  let private sortCharacters (s: string) =
    if s.Length > 1 then
      new string (s.ToCharArray() |> Array.sort)
    else
      s

  let init initializedFromQuery s : Model * Cmd<Msg> =
    { input = s
      sorted = sortCharacters s
      // history = history
      initializedFromQuery = initializedFromQuery },
    []

  let update msg model : Model * Cmd<Msg> =
    match msg with
    | Initialized when model.initializedFromQuery -> { model with initializedFromQuery = false }, []
    | Initialized -> model, []
    // | Memo when model.input = "" -> model, []
    // | Memo -> { model with history = (model.input, model.sorted) :: model.history }, []
    // | ClearHistory when model.history.Length = 0 -> model, []
    // | ClearHistory -> { model with history = List.empty }, []
    | ChangeStr str ->
      if model.input = str then
        model, []
      else
        let sorted = new string (str.ToCharArray() |> Array.sort)

        { model with
            input = str
            sorted = sorted
            initializedFromQuery = false },
        Navigation.modifyUrl (Page.toPath (SortCharacters str))
