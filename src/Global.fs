module FablePlayground.Global

open Fable.Core.JS

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
    | SortCharacters s ->
      let inputEncoded = encodeURIComponent s
      sprintf "#%s?value=%s" Hash.SortCharacters inputEncoded

module PageParser =
  open Elmish.UrlParser

  let mappingSortCharactersValue =
    Option.map decodeURIComponent
    >> Option.defaultValue ""
    >> SortCharacters

  let parseSortCharactersValue () =
    s Hash.SortCharacters <?> stringParam "value"

  let pageParser: Parser<Page -> Page, Page> =
    oneOf
      [ map About (s Hash.About)
        map (mappingSortCharactersValue) (parseSortCharactersValue ()) ]
