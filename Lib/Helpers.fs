module PostMaster.Lib.Helpers

open System

let join sep (items: string seq) =
    String.Join (sep, items)
    
let split (sep: string) (item: string) =
    item.Split (sep, StringSplitOptions.RemoveEmptyEntries)
    
let takeAllButLast (items: 'a seq) =
    let cnt = items |> Seq.length
    items
    |> Seq.take (cnt - 1)
    
let trim (item: string) =
    item.Trim ()
