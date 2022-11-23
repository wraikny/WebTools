module Shared

#r "netstandard"
#r "nuget: FSharp.Json"

[<Literal>]
let UrlBase = @"https://yakkun.com"

[<Literal>]
let DownloadUrl = UrlBase + "/sv/stats_list.htm?mode=rank_battle"

[<Literal>]
let DataPath = __SOURCE_DIRECTORY__ + @"/../data/stats_list.html"

[<Literal>]
let JsonPath =
  __SOURCE_DIRECTORY__
  + @"/../../public/stats_list.json"

type Pokemon =
  { no: int
    name: string
    url: string
    h: int
    a: int
    b: int
    c: int
    d: int
    s: int
    sum: int }

module Pokemon =
  open FSharp.Json

  let toJson (ps: Pokemon []) = Json.serialize (ps)

  let fromJson (s: string) : Pokemon [] = Json.deserialize (s)

  open System.IO

  let loadAsync () =
    task {
      let! json = File.ReadAllTextAsync(JsonPath)
      return fromJson json
    }
    |> Async.AwaitTask

  let load () = loadAsync () |> Async.RunSynchronously

module Utils =
  let (|Contains|_|) (x: string) (s: string) =
    if s.Contains(x) then
      Some Contains
    else
      None

  let groupBy (f: 'a -> 'b * 'c) (arr: 'a []) : ('b * 'c []) [] =
    arr
    |> Array.map f
    |> Array.groupBy fst
    |> Array.map (fun (k, xs) -> (k, xs |> Array.map snd))
