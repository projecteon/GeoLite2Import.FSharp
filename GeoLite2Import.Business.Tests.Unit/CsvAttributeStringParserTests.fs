module CsvAttributeStringParserTests

open System
open Xunit
open GeoLite2Import.Business.CsvAttributeStringParser
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

let fakeFullCsv : string = "oslo,norway"
let fakeInCompleteCsv : string = "oslo"

let OkMappedType = new FakeMapType (city = "oslo", country = "norway")
let OkIncompleteMappedType = new FakeMapType (city = "oslo")
let NotOkMappedType = new FakeMapType (city = "norway", country = "oslo")

[<Fact>]
let ``When csv string contains right amount of columns, the columns are mapped according to column index attribute`` () =
    let resultMap = Parse<FakeMapType>(fakeFullCsv);
    Assert.Equal(OkMappedType, resultMap)

[<Fact>]
let ``When csv string contains right amount of columns, the columns are not mapped wrongly according to column index attribute`` () =
    let resultMap = Parse<FakeMapType>(fakeFullCsv);
    Assert.NotEqual(NotOkMappedType, resultMap)

[<Fact>]
let ``When csv string is incomplete, the availble columns are mapped according to column index attribute`` () =
    let resultMap = Parse<FakeMapType>(fakeInCompleteCsv);
    Assert.Equal(OkIncompleteMappedType, resultMap)