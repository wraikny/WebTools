namespace FablePlayground.Pages.SortCharacters

open Elmish
open Elmish.Navigation

open FablePlayground.Global

type Msg = ChangeStr of string

type Model =
  { input: string
    sorted: string
    isUpdated: bool }

module Model =
  let private sortCharacters (s: string) =
    if s.Length > 1 then
      new string (s.ToCharArray() |> Array.sort)
    else
      s

  let init s : Model * Cmd<Msg> =
    { input = s
      sorted = sortCharacters s
      isUpdated = false },
    []

  let update msg model : Model * Cmd<Msg> =
    match msg with
    | ChangeStr str ->
      if model.input = str then
        model, []
      else
        let sorted = new string (str.ToCharArray() |> Array.sort)

        { input = str
          sorted = sorted
          isUpdated = true },
        Navigation.modifyUrl (Page.fromString (SortCharacters str))
