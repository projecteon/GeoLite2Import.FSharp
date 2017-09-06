// Learn more about F# at http://fsharp.org

open System
open System.IO
open System.Net
open System.Numerics
open GeoLite2Import.Business.CsvTranslator
open GeoLite2Import.Business.Models

let Ipv4CityBlocksImportFilePath = @"geolite2\GeoLite2-City-Blocks-IPv4-example.csv";
let Ipv4CityLocationsImportFilePath = @"geolite2\GeoLite2-City-Locations-en.csv";
let Ipv4CountryBlocksImportFilePath = @"geolite2\GeoLite2-Country-Blocks-IPv4-example.csv";
let Ipv4CountyLocationsImportFilePath = @"geolite2\GeoLite2-Country-Locations-en-example.csv";
let WorldCitiesPopImportFilePath = @"geolite2\worldcitiespop-example.csv";

let translatePart(ipPart: string) =
    ipPart
    |> int
    |> sprintf "%03i"
    

let translate(ipAddress: IPAddress) =
    ipAddress.ToString().Split('.')
    |> Seq.map translatePart
    |> Seq.reduce (fun x y -> x + y)
    |> BigInteger.Parse


let Import<'a when 'a: (new: unit -> 'a)>(filePath: string) =
    let csvLines = File.ReadAllLines(filePath)
    if csvLines.Length = 0 then
        printfn "Reading csv file failed, no lines read!"
        [||]
    else
        match Translate<'a>(csvLines) with
        | Success s -> s
        | Failure f -> 
            printfn "%s" f
            [||]
    
let LogImport<'a when 'a: (new: unit -> 'a)>(filePath: string) =
    let result = Import<'a>(filePath)
    printfn "map result %i" result.Length
    result 
    |> Array.iteri (fun i f -> printfn "line: %i - %A" i f) 
    |> ignore

let LogIp(ipvBlock: GeoLite2CityBlock) =
    let ipnetwork = IPNetwork.Parse(ipvBlock.network)
    printfn "%s, %A - %A, %A - %A" ipvBlock.network ipnetwork.FirstUsable ipnetwork.LastUsable (translate ipnetwork.FirstUsable) (translate ipnetwork.LastUsable)

let LogIpRanges() =
    let result = Import<GeoLite2CityBlock>(Ipv4CityBlocksImportFilePath)
    result 
    |> Array.iter (fun f -> LogIp f)    
    |> ignore

[<EntryPoint>]
let main argv =
    printfn "GeoLite2Import start"
    // LogImport<GeoLite2CityBlock>(Ipv4CityBlocksImportFilePath)
    LogIpRanges()
    0 // return an integer exit code