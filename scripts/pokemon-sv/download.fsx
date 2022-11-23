#r "netstandard"

#load "shared.fsx"

open System.Net.Http
open System.IO
open System.Text

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)

let client = new HttpClient()

do
  task {
    let encoding = Encoding.GetEncoding("euc-jp")

    let! res = client.GetAsync(Shared.DownloadUrl)
    use! stream = res.Content.ReadAsStreamAsync()
    use reader = new StreamReader(stream, encoding, true) :> TextReader
    let! html = reader.ReadToEndAsync()
    do! File.WriteAllTextAsync(Shared.DataPath, html)
  }
  |> Async.AwaitTask
  |> Async.RunSynchronously
