module PostMaster.Lib.FileNameParser

open System
open Helpers

let parse (fileName: string) =
    let nameParts =
        fileName
        |> split "-"
        
    let date =
        nameParts
        |> Seq.take 3
        |> join "-"
        |> DateOnly.Parse
        
    let id =
        nameParts
        |> Seq.skip 3
        |> join "-"
        |> split "."
        |> takeAllButLast
        |> join "."
        
    {
        Id = id
        Date = date
    }