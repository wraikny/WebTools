module FablePlayground.Global

module private Hash =
  [<Literal>]
  let About = "about"

  [<Literal>]
  let SortCharacters = "sort-characters"

type Page =
  | About
  | SortCharacters of string

module Page =
  let fromString page =
    match page with
    | About -> sprintf "#%s" Hash.About
    | SortCharacters s -> sprintf "#%s?value=%s" Hash.SortCharacters s

module PageParser =
  open Elmish.UrlParser

  let pageParser: Parser<Page -> Page, Page> =
    oneOf
      [ map About (s Hash.About)
        map (Option.defaultValue "" >> SortCharacters) (s Hash.SortCharacters <?> stringParam "value") ]
