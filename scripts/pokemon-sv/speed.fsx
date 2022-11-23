#load "shared.fsx"

open System.IO

let [<Literal>] MinimumStatusSum: int = 430

type SpeedKind =
  | Saisoku of single
  | Junsoku of single
  | Mufuri of single
  | Saichi of single

let speedKinds = [|
  Saisoku 2.00f
  Saisoku 1.50f
  Saisoku 1.00f
  Saisoku 0.75f

  Junsoku 2.00f
  Junsoku 1.50f
  Junsoku 1.00f
  Junsoku 0.75f

  Mufuri 1.50f
  Mufuri 1.00f
  Mufuri 0.75f

  Saichi 1.00f
  Saichi 0.75f
  Saichi 0.50f
|]

let calc ({ s = s }: Shared.Pokemon) (speed) =
  let f v e = (s * 2 + v + e / 4) * 50 / 100 + 5 |> single
  match speed with
  | Saisoku x -> x * 1.1f * f 31 252
  | Junsoku x -> x * 1.0f * f 31 252
  | Mufuri x  -> x * 1.0f * f 31 000
  | Saichi x  -> x * 0.9f * f 00 000
  |> floor |> int

// do
//   let pokemons =
//     Shared.Pokemon.load ()
//     |> Array.filter (fun p -> p.sum >= MinimumStatusSum)
//     |> Array.sortByDescending (fun p -> p.sum)

//   let speeds =
//     [|
//       for p in pokemons do
//         for sk in speedKinds do
//           let actualSpeed = calc p sk
//           (actualSpeed, (sk, p.s, p))
//     |]
//     |> Shared.Utils.groupBy id
//     |> Array.map (fun (a, ps) ->
//       let ps =
//         ps
//         |> Shared.Utils.groupBy (fun (sk, s, n) -> ((sk, s), n))
//         |> Array.sortByDescending (fun ((_, s), _) -> s)
//       (a, ps)
//     )
//     |> Array.sortByDescending fst

//   let createText (enabledUrl: bool) =
//     let list =
//       speeds
//       |> Array.map (fun (actualSpeed, groups) ->
//         sprintf "* 実数値%d\n%s" actualSpeed (
//           groups |> Array.map (fun ((sk, ss), ps) ->
//             (match sk with
//               | Saisoku r -> sprintf "  * 最速%d族*%.2f\n%s" ss r
//               | Junsoku r -> sprintf "  * 準速%d族*%.2f\n%s" ss r
//               | Mufuri r -> sprintf "  * 無振%d族*%.2f\n%s" ss r
//               | Saichi r -> sprintf "  * 最遅%d族*%.2f\n%s" ss r
//             ) (
//               ps |> Array.map (fun p ->
//                 sprintf "    * %s"
//                   (if enabledUrl then Shared.Pokemon.toExternalLink p else p.name)
//               )
//               |> String.concat "\n"
//             )
//           ) |> String.concat "\n"
//         )
//       )
//       |> String.concat "\n"

//     let text = $"""
// # ポケモンSV素早さ早見表

// ランクバトルで使用可能（？）なポケモンのうち、
// 合計種族値430以上を、補正込での実数値順に並べました。

// %s{list}
// """
//     text

//   File.WriteAllText(Shared.SpeedPath, createText false)
//   File.WriteAllText(Shared.SpeedLinkedPath, createText true)
//   printfn "finished!"

//   pokemons
//   |> Array.length
//   |> printfn "ポケモン数: %d"

//   speeds
//   |> Array.sumBy (snd >> Array.sumBy (snd >> Array.length))
//   |> printfn "データ数: %d"
