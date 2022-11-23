#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"

#load ".fake/build.fsx/intellisense.fsx"


open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

[<AutoOpen>]
module Utils =
  let dotnet cmd arg =
    let res = DotNet.exec id cmd arg

    if not res.OK then
      failwithf "Failed 'dotnet %s %s'" cmd arg

let formatTargets =
  !! "src/**/*.fs"
  -- "src/*/obj/**/*.fs"
  -- "src/*/bin/**/*.fs"
  ++ "build.fsx"
  ++ "scripts/**/*.fsx"

Target.initEnvironment ()

Target.create
  "Format"
  (fun _ ->
    formatTargets
    |> String.concat " "
    |> dotnet "fantomas"
  )

Target.create
  "Format.Check"
  (fun _ ->
    formatTargets
    |> String.concat " "
    |> sprintf "--check %s"
    |> dotnet "fantomas"
  )

Target.create "None"

Target.runOrDefault "None"
