namespace WebTools.Pages.SortCharacters

open Elmish
open Elmish.Navigation

open WebTools.Global

type Msg =
  | ChangeStr of string
  | Initialized

type Model =
  { input: string
    sorted: string
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
      initializedFromQuery = initializedFromQuery },
    []

  let toPage model = SortCharacters model.input

  let update msg model : Model * Cmd<Msg> =
    match msg with
    | Initialized when model.initializedFromQuery -> { model with initializedFromQuery = false }, []
    | Initialized -> model, []
    | ChangeStr str ->
      if model.input = str then
        model, []
      else
        let sorted = new string (str.ToCharArray() |> Array.sort)

        { model with
            input = str
            sorted = sorted
            initializedFromQuery = false },
        Navigation.modifyUrl (toPage model |> Page.toPath)
