module WebTools.Global

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
  let toPath page =
    match page with
    | About -> sprintf "#%s" Hash.About
    | SortCharacters "" -> sprintf "#%s" Hash.SortCharacters
    | SortCharacters s ->
      let inputEncoded = encodeURIComponent s
      sprintf "#%s?value=%s" Hash.SortCharacters inputEncoded

module Title =
  [<Literal>]
  let Base = "wraikny's WebTools"

  [<Literal>]
  let About = Base

  [<Literal>]
  let SortCharacters = "文字をソートするやつ"

  let SortCharactersTitle = sprintf "%s - %s" SortCharacters Base

  let toPageTitle =
    function
    | Page.About -> Base
    | Page.SortCharacters _ -> SortCharactersTitle


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
