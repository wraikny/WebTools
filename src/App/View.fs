module FablePlayground.App.View

open Fable.React
open Fable.React.Props

open FablePlayground
open FablePlayground.Global
open FablePlayground.Pages
open FablePlayground.App

let menuItem label page currentPage =
  ul
    [ ClassName "menu-list" ]
    [ li
        []
        [ a
            [ classList [ "is-active", page = currentPage ]
              Href(Page.toPath page) ]
            [ str label ] ] ]

let menu model =
  aside
    [ ClassName "menu" ]
    [ p [ ClassName "menu-label" ] [ str "General" ]
      menuItem "このサイトについて" About model.currentPage
      menuItem "文字をソートするやつ" (SortCharacters model.sortCharacters.input) model.currentPage ]

let root model (dispatch: Msg -> unit) =
  let pageHtml page =
    match page with
    | Page.About -> About.View.root
    | Page.SortCharacters _ -> SortCharacters.View.root model.sortCharacters (Utils.lift >> dispatch)

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
                  div [ ClassName "column" ] [ pageHtml model.currentPage ] ] ] ] ]
