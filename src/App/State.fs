namespace FablePlayground.App

open Elmish
open Elmish.Navigation
open Browser

open FablePlayground
open FablePlayground.Global
open FablePlayground.Pages

type Msg =
  | SortCharactersMsg of SortCharacters.Msg
  static member Lift(s) = SortCharactersMsg s

type Model =
  { sortCharacters: SortCharacters.Model
    currentPage: Page }
  static member Get(m) = m.sortCharacters
  static member Set(m, v) = { m with sortCharacters = v }

module Model =
  let inline updateChild f model =
    let child = Utils.get model
    let (updated, cmd) = f child
    model |> Utils.set updated, Cmd.map Utils.lift cmd

  let urlUpdate (result: Page option) model =
    match result with
    | None ->
      console.error ("Error parsing url")
      model, Navigation.modifyUrl (Page.toPath model.currentPage)

    | Some page ->
      let model = { model with currentPage = page }

      match page with
      | SortCharacters s ->
        model
        |> updateChild (fun _ -> SortCharacters.Model.init s)
      | _ -> model, []

  let init result : Model * Cmd<Msg> =
    let (sortCharacters, sortCharactersCmd) =
      let input =
        result
        |> function
          | Some (SortCharacters s) -> s
          | _ -> ""

      SortCharacters.Model.init input

    let (model, cmd) =
      urlUpdate
        result
        { sortCharacters = sortCharacters
          currentPage = About }

    model,
    Cmd.batch
      [ cmd
        Cmd.map SortCharactersMsg sortCharactersCmd ]

  let update msg model : Model * Cmd<Msg> =
    match msg with
    | SortCharactersMsg m ->
      let model, cmd =
        model
        |> updateChild (SortCharacters.Model.update m)

      { model with currentPage = SortCharacters model.sortCharacters.input }, cmd
