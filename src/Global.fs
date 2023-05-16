module WebTools.Global

open Fable.Core.JS

module private Hash =
  [<Literal>]
  let About = "about"

  [<Literal>]
  let SortCharacters = "sort-characters"

  [<Literal>]
  let VerbGenerator = "verb-generator"

type Page =
  | About
  | SortCharacters of string
  | VerbGenerator of int option

module Page =
  let toPath page =
    match page with
    | About -> sprintf "#%s" Hash.About
    | SortCharacters "" -> sprintf "#%s" Hash.SortCharacters
    | SortCharacters s ->
      let inputEncoded = encodeURIComponent s
      sprintf "#%s?value=%s" Hash.SortCharacters inputEncoded
    | VerbGenerator None -> sprintf "#%s" Hash.VerbGenerator
    | VerbGenerator (Some n) -> sprintf "#%s?count=%d" Hash.VerbGenerator n

module Title =
  [<Literal>]
  let Base = "wraikny's WebTools"

  [<Literal>]
  let About = Base

  [<Literal>]
  let SortCharacters = "文字列ソート"

  [<Literal>]
  let VerbGenerator = "ランダム動詞ジェネレータ"

  let SortCharactersTitle = sprintf "%s - %s" SortCharacters Base

  let toPageTitle =
    function
    | Page.About -> Base
    | Page.SortCharacters _ -> SortCharactersTitle
    | Page.VerbGenerator _ -> VerbGenerator


module PageParser =
  open Elmish.UrlParser

  let mappingSortCharactersValue =
    Option.map decodeURIComponent
    >> Option.defaultValue ""
    >> SortCharacters

  let pageParser: Parser<Page -> Page, Page> =
    oneOf
      [ map About (s Hash.About)
        map (mappingSortCharactersValue) (s Hash.SortCharacters <?> stringParam "value")
        map VerbGenerator (s Hash.VerbGenerator <?> intParam "count") ]
