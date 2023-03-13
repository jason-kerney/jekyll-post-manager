module PostMaster.Lib.YamlParser

open System
open Helpers

let parseSegment yaml =
    let parts =
        yaml
        |> split ": "
        
    let title = parts |> Seq.head
    let value =
        parts
        |> Seq.skip 1
        |> Seq.head
    {
        Id = title
        Value = value
    }

let parse yaml =
    let segments = 
        yaml
        |> split "---"
        |> Seq.head
        |> split "\n"
        |> Seq.map trim
        |> Seq.filter (fun item -> 0 < item.Length)
        |> Seq.map parseSegment
        |> Seq.filter (fun seg ->
            seg.Id = "title" || seg.Id = "date"
        )
        
    let title =
        segments
        |> Seq.filter (fun seg -> seg.Id = "title")
        |> Seq.head
        |> fun seg -> seg.Value
        
    let dateTime =
        segments
        |> Seq.filter (fun seg -> seg.Id = "date")
        |> Seq.head
        |> fun item -> item.Value
        |> DateTime.Parse
        
    {
        Id = title
        DateTime = dateTime
    }
    