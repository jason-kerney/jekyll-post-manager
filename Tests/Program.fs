namespace FeldSpar.Console.Tests
open FeldSpar.Framework
open FeldSpar.Framework.ConsoleRunner
open FeldSpar.Framework.Engine
open FeldSpar.Framework.Verification.ApprovalsSupport
open ApprovalTests

module Program =
      
    let any values =
        match values with
        | [] -> false
        | _ -> true
        
    //let ``Setup Global Reports`` = 
    let Setup_Global_Reports = 
        Config(fun () -> 
        { 
            Reporters = [
                            fun _ -> 
                                    Searching
                                        |> findFirstReporter<Reporters.DiffReporter>
                                        |> findFirstReporter<Reporters.WinMergeReporter>
                                        |> findFirstReporter<Reporters.InlineTextReporter>
                                        |> findFirstReporter<Reporters.AllFailingTestsClipboardReporter>
                                        |> unWrapReporter
                                            
                            fun _ -> Reporters.ClipboardReporter() :> Core.IApprovalFailureReporter;

                            fun _ -> Reporters.QuietReporter() :> Core.IApprovalFailureReporter;
                        ]
        })

    let currentToken =
        let assembly = System.Reflection.Assembly.GetExecutingAssembly()
        let assemblyPath =
            let codebase = assembly.Location
            let uriBuilder = System.UriBuilder codebase
            let path = System.Uri.UnescapeDataString uriBuilder.Path
            System.IO.Path.GetDirectoryName path

        { new IToken with
            member this.AssemblyPath = assemblyPath
            member this.AssemblyName = System.IO.Path.GetFileName assemblyPath
            member this.Assembly = assembly
            member this.IsDebugging = false
            member this.GetExportedTypes () = assembly.GetExportedTypes()
        }

    let framework, results = runAndReportFailure UseAssemblyConfiguration ShowDetails currentToken
    let successes = results |> List.filter (fun item -> item.TestResults = Success)
    let failures = results |> List.filter (fun item -> item.TestResults = Success |> not)

    printfn $"running %s{framework}"    

    failures
    |> List.iter (fun item ->
        printfn $"\t Test: (Failure) %s{item.TestContainerName} %s{item.TestName}"
    )
    
    if failures |> any
    then printfn ""
    
    successes
    |> List.iter (fun item ->
        printfn $"\t Test: (Success) %s{item.TestContainerName} %s{item.TestName}"
    )
    
    printf "\nDone!"
    
    failures |> List.length |> exit
