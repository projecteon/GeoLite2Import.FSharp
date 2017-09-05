module GeoLite2Import.Business.CsvTranslator

open GeoLite2Import.Business.CsvSchemaValidator
open GeoLite2Import.Business.CsvAttributeStringParser

type Result<'TSuccess,'TFailure> = 
    | Success of 'TSuccess
    | Failure of 'TFailure

let NoSchemaFailure = Failure "csv contains no schema"
let NoDataFailure = Failure "csv contains no data"

let Translate<'a when 'a: (new: unit -> 'a)>(csvLines: string[]) =
    if csvLines.Length = 0 then NoSchemaFailure
    else if csvLines.Length = 1 then NoDataFailure
    else
        let schema = csvLines.[0];
        if IsSchemaValid<'a>(schema) then Success <| (csvLines |> Seq.skip 1 |> Seq.map Parse<'a> |> Seq.toArray)
        else Failure <| sprintf "schema '%s' is not valid for %s" schema typeof<'a>.Name