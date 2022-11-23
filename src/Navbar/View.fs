module WebTools.Navbar.View

open Fable.React
open Fable.React.Props

let navButton classy href faClass txt =
  p
    [ ClassName "control" ]
    [ a
        [ ClassName(sprintf "button %s" classy)
          Href href
          Target "_blank"
          Rel "noopener noreferrer" ]
        [ span [ ClassName "icon" ] [ i [ ClassName(sprintf "fab %s" faClass) ] [] ]
          span [] [ str txt ] ] ]

let navButtons =
  span
    [ ClassName "navbar-item" ]
    [ div
        [ ClassName "field is-grouped" ]
        [ navButton "twitter" "https://twitter.com/wraikny" "fa-twitter" "Twitter" (* navButton "github" "https://github.com/wraikny/WebTools" "fa-github" "GitHub" *)  ] ]

let root =
  nav
    [ ClassName "navbar is-dark" ]
    [ div
        [ ClassName "container" ]
        [ div [ ClassName "navbar-brand" ] [ h1 [ ClassName "navbar-item title is-4" ] [ str "wraikny's WebTools" ] ]
          div [ ClassName "navbar-end" ] [ navButtons ] ] ]
