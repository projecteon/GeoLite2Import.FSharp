module CsvTranslatorTests

open System
open Xunit
open GeoLite2Import.Business.CsvTranslator
open GeoLite2Import.Business.Models

type FakeMapType () =
    [<CsvColumnAttribute(0)>]
    member val city = "" with get,set
    [<CsvColumnAttribute(1)>]
    member val country = "" with get,set

    override this.Equals(obj) =
        match obj with
        | :? FakeMapType as ent -> 
            this.city = ent.city && this.country = ent.country   // no null checking needed
        | _ ->  false // all other cases

    /// Needed for custom equality
    override this.GetHashCode() =
        hash (this.city + this.country)

    /// Implement custom equality
    interface IEquatable<FakeMapType> with
        member this.Equals(ent) =
            this.city = ent.city && this.country = ent.country  // no null checking needed

let fakeCsv : string[] = [|"city,country";"oslo,norway"|]
let fakeMultipleCsv : string[] = [|"city,country";"oslo,norway";"amsterdam,netherlands"|]
let OkMappedType = new FakeMapType (city = "oslo", country = "norway")
let OkMappedType2 = new FakeMapType (city = "amsterdam", country = "netherlands")

[<Fact>]
let ``When csv contains both valid schema and data, a result success with mapped data is returned`` () =
    let resultMap = Translate<FakeMapType>(fakeCsv);    
    let expected = Success [|OkMappedType|];
    Assert.Equal(expected, resultMap)

[<Fact>]
let ``When csv contains both valid schema and multiple data, a result success with mapped multiple data is returned`` () =
    let resultMap = Translate<FakeMapType>(fakeMultipleCsv);    
    let expected = Success [|OkMappedType;OkMappedType2|];
    Assert.Equal(expected, resultMap)

[<Fact>]
let ``When csv contains no schema, a result failure returned`` () =
    let resultMap = Translate<FakeMapType>([||]);    
    Assert.Equal(NoSchemaFailure, resultMap)

[<Fact>]
let ``When csv contains no data, a result failure returned`` () =
    let resultMap = Translate<FakeMapType>([|"city,country"|]);    
    Assert.Equal(NoDataFailure, resultMap)

[<Fact>]
let ``When csv contains invalid schema, a result failure returned`` () =
    let resultMap = Translate<FakeMapType>([|"city,country2";"oslo,norway"|]);    
    let expected = Failure <| sprintf "schema '%s' is not valid for %s" "city,country2" typeof<FakeMapType>.Name
    Assert.Equal(expected, resultMap)