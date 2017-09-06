// Learn more about F# at http://fsharp.org

open System
open System.IO
open GeoLite2Import.Business.CsvTranslator
open GeoLite2Import.Business.Models

let Ipv4BCityBlocksImportFilePath = @"geolite2\GeoLite2-City-Blocks-IPv4-example.csv";
let Ipv4CityLocationsImportFilePath = @"geolite2\GeoLite2-City-Locations-en.csv";
let Ipv4CountryBlocksImportFilePath = @"geolite2\GeoLite2-Country-Blocks-IPv4-example.csv";
let Ipv4CountyLocationsImportFilePath = @"geolite2\GeoLite2-Country-Locations-en-example.csv";
let WorldCitiesPopImportFilePath = @"geolite2\worldcitiespop-example.csv";

let Import<'a when 'a: (new: unit -> 'a)>(filePath: string) =
    let csvLines = File.ReadAllLines(filePath)
    if csvLines.Length = 0 then
        printfn "Reading csv file failed, no lines read!"
        [||]
    else
        csvLines |> Seq.map (fun f -> printf "%s" f) |> ignore
        match Translate<'a>(csvLines) with
        | Success s -> s
        | Failure f -> 
            printfn "%s" f
            [||]
    
let LogImport<'a when 'a: (new: unit -> 'a)>(filePath: string) =
    let result = Import<'a>(filePath)
    result |> Seq.map (fun f -> printf "%A" f)

[<EntryPoint>]
let main argv =
    printfn "GeoLite2Import start"
    LogImport<GeoLite2CityBlock>(Ipv4BCityBlocksImportFilePath) |> ignore
    0 // return an integer exit code
