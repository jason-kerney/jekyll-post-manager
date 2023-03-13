module PostMaster.Lib.FileNameParser

open System

let private join sep (items: string seq) =
    String.Join (sep, items)
    
let private split (sep: string) (item: string) =
    item.Split (sep, StringSplitOptions.RemoveEmptyEntries)
    
let private takeAllButLast (items: 'a seq) =
    let cnt = items |> Seq.length
    items
    |> Seq.take (cnt - 1)

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