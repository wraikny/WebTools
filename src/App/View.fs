module WebTools.App.View

open Fable.React
open Fable.React.Props

open WebTools
open WebTools.Global
open WebTools.Pages
open WebTools.App

let menuItem label faClass page currentPage =
  ul
    [ ClassName "menu-list" ]
    [ li
        []
        [ a
            [ classList
                [ "is-active", page = currentPage
                  "icon-text", true ]
              Href(Page.toPath page) ]
            [ span [ ClassName "icon" ] [ i [ ClassName(sprintf "fa %s" faClass) ] [] ]
              span [] [ str label ]

              ] ] ]

let menu model =
  aside
    [ ClassName "menu" ]
    [ p [ ClassName "menu-label" ] [ str "General" ]

      menuItem "ホーム" "fa-home" About model.currentPage

      menuItem
        Title.SortCharacters
        "fa-arrow-down-a-z"
        (SortCharacters.Model.toPage model.sortCharacters)
        model.currentPage

      menuItem Title.VerbGenerator "fa-dice" (VerbGenerator.Model.toPage model.verbGenerator) model.currentPage ]

let root model (dispatch: Msg -> unit) =
  let pageHtml page =
    match page with
    | Page.About -> About.View.root
    | Page.SortCharacters _ -> SortCharacters.View.root model.sortCharacters (SortCharactersMsg >> dispatch)
    | Page.VerbGenerator _ -> VerbGenerator.View.root model.verbGenerator (VerbGeneratorMsg >> dispatch)

  div
    []
    [ Navbar.View.root
      div
        [ ClassName "section" ]
        [ div
            [ ClassName "container" ]
            [ div
                [ ClassName "columns" ]
                [ div [ ClassName "column is-3" ] [ menu model ]
                  div [ ClassName "column is-6" ] [ pageHtml model.currentPage ] ] ] ] ]
