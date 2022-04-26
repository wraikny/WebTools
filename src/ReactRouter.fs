module ReactRouter

open Fable.Core
open Fable.Core.JsInterop
open Fable.React

type BrowserRouterProps =
  | BaseName of string
  | GetUserConfirmation of (string -> (bool -> unit) -> unit)
  | ForceRefresh of bool
  | KeyLength of int

let inline BrowserRouter (props: BrowserRouterProps list) (elems: ReactElement list) : ReactElement =
  ofImport "BrowserRouter" "react-router-dom" (keyValueList CaseRules.LowerFirst props) elems

type ToObject =
  { pathname: string
    search: string
    hash: string
    state: string }

type LinkProps =
  | To of U3<string, ToObject, (string -> string)>
  | Replace of bool

let inline Link (props: LinkProps list) (elems: ReactElement list) : ReactElement =
  ofImport "Link" "react-router-dom" (keyValueList CaseRules.LowerFirst props) elems

type RouteProps = Path of string

let inline Route (props: RouteProps list) (elems: ReactElement list) : ReactElement =
  ofImport "Route" "react-router-dom" (keyValueList CaseRules.LowerFirst props) elems

let inline Switch (props: unit list) (elems: ReactElement list) : ReactElement =
  ofImport "Switch" "react-router-dom" (keyValueList CaseRules.LowerFirst props) elems
