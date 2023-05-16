namespace WebTools.App

open Elmish
open Elmish.Navigation
open Browser

open WebTools
open WebTools.Global
open WebTools.Pages

type Msg =
  | SortCharactersMsg of SortCharacters.Msg
  | VerbGeneratorMsg of VerbGenerator.Msg
  static member Lift(s) = SortCharactersMsg s
  static member Lift(s) = VerbGeneratorMsg s

type Model =
  { sortCharacters: SortCharacters.Model
    verbGenerator: VerbGenerator.Model
    currentPage: Page }
  static member Get(m, _: SortCharacters.Model) = m.sortCharacters
  static member Get(m, _: VerbGenerator.Model) = m.verbGenerator
  static member Set(m, v) = { m with sortCharacters = v }
  static member Set(m, v) = { m with verbGenerator = v }

module Model =
  let inline updateChild (f: ^a -> ^a * Cmd< ^b >) (model: Model) : Model * Cmd<Msg> =
    let child = Utils.get model
    let (updated, cmd) = f child
    model |> Utils.set updated, Cmd.map Utils.lift cmd

  let mapPage f model = { model with currentPage = f model }

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
        |> updateChild (fun _ -> SortCharacters.Model.init true s)
      | VerbGenerator n ->
        model
        |> updateChild (fun _ -> VerbGenerator.Model.init n)
      | _ -> model, []
      |> fun (model, cmd) ->
           (model,
            (Sub.setTitle (Title.toPageTitle model.currentPage))
            :: cmd)

  let init result : Model * Cmd<Msg> =
    let (sortCharacters, sortCharactersCmd) = SortCharacters.Model.init false ""
    let (verbGenerator, VerbGeneratorCmd) = VerbGenerator.Model.init None

    let (model, cmd) =
      urlUpdate
        result
        { sortCharacters = sortCharacters
          verbGenerator = verbGenerator
          currentPage = About }

    model,
    Cmd.batch
      [ cmd
        Cmd.map SortCharactersMsg sortCharactersCmd
        Cmd.map VerbGeneratorMsg VerbGeneratorCmd ]

  let update msg model : Model * Cmd<Msg> =
    match msg with
    | SortCharactersMsg m ->
      model
      |> updateChild (SortCharacters.Model.update m)
      |> Utils.mapFst (mapPage (Utils.get >> SortCharacters.Model.toPage))
    | VerbGeneratorMsg m ->
      model
      |> updateChild (VerbGenerator.Model.update m)
      |> Utils.mapFst (mapPage (Utils.get >> VerbGenerator.Model.toPage))
