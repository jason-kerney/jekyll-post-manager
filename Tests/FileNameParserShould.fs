module PostMaster.Tests.``File Name Parser``

open System
open FeldSpar.Framework
open FeldSpar.Framework.Verification
open PostMaster.Lib

let ``Should parse Id and Date`` =
    Test(fun _ ->
        "2021-04-16-ideas.md"
        |> FileNameParser.parse
        |> expectsToBe {
            Id = "ideas"
            Date = DateOnly.Parse "2021-04-16"
        }
    )
    
let ``Should parse different Id and Date``  =
    Test(fun _ ->
        "1984-01-02-hello-world.md"
        |> FileNameParser.parse
        |> expectsToBe {
            Id = "hello-world"
            Date = DateOnly.Parse "1984-01-02"
        }
    )
    
let ``Should parse Id with multiple dots``  =
    Test(fun _ ->
        "1900-10-12-life.the.universe.and.everything.md"
        |> FileNameParser.parse
        |> expectsToBe {
            Id = "life.the.universe.and.everything"
            Date = DateOnly.Parse "1900-10-12"
        }
    )