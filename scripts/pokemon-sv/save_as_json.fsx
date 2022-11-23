#r "netstandard"
#r "nuget: FSharp.Data"

#load "shared.fsx"

open System.Text
open System.IO

open FSharp.Data

type Distincted = Distincted of string [] * string

let private distinctedPokemons =
  let d s = Distincted([| s |], s)

  [| Distincted([| "ケンタロス"; "パルデア" |], "ケンタロス(パルデア)")

     d "オドリドリ"
     d "バスラオ"
     d "ロトム"
     d "イッカネズミ"
     d "ウーラオス"
     d "シャリタツ"
     d "ノココッチ"
     d "ストリンダー" |]

let private parseName (node: HtmlNode) : string =
  let name =
    node.InnerText().Split('\n')
    |> Array.map (fun s ->
      StringBuilder(s.Trim())
        .Replace("のすがた", "")
        .Replace("の姿", "")
        .Replace("フォルム", "")
        .ToString()
    )
    |> String.concat ""

  distinctedPokemons
  |> Array.tryFind (fun (Distincted (targets, _)) ->
    targets
    |> Array.forall (fun t -> name.Contains(t))
  )
  |> function
    | None -> name
    | Some (Distincted (_, replaced)) -> replaced


let private nodesToPokemon =
  function
  | [| no: HtmlNode; name; h; a; b; c; d; s; sum |] ->
    let name =
      name
      |> HtmlNode.descendantsNamed false [ "a" ]
      |> Seq.head

    let n = parseName name

    let url =
      name
      |> HtmlNode.tryGetAttribute "href"
      |> fun n -> $"%s{Shared.UrlBase}%s{(Option.get n).Value()}"

    { no = no.InnerText() |> int
      name = n
      url = url
      h = h.InnerText() |> int
      a = a.InnerText() |> int
      b = b.InnerText() |> int
      c = c.InnerText() |> int
      d = d.InnerText() |> int
      s = s.InnerText() |> int
      sum = sum.InnerText() |> int }: Shared.Pokemon
  | x -> failwithf "failed: %A" (x |> Array.map (fun x -> x.InnerText()))

let private parse (doc: HtmlDocument) =
  doc.Body()
  |> HtmlNode.descendantsNamed true [ "table" ]
  |> Seq.head
  |> HtmlNode.descendantsNamed false [ "tbody" ]
  |> Seq.head
  |> HtmlNode.descendantsNamed true [ "tr" ]
  |> Seq.map (
    HtmlNode.descendantsNamed false [ "td" ]
    >> Seq.toArray
    >> nodesToPokemon
  )

let private load () : Shared.Pokemon [] =
  let pokemons =
    HtmlDocument.Load(Shared.DataPath)
    |> parse
    |> Seq.toArray

  pokemons |> Array.distinctBy (fun p -> p.name)

do
  let pokemons = load ()
  let json = Shared.Pokemon.toJson pokemons
  File.WriteAllText(Shared.JsonPath, json)
