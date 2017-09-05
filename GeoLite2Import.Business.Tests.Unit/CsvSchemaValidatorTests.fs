module CsvSchemaValidatorTests

open System
open Xunit
open GeoLite2Import.Business.CsvSchemaValidator
open GeoLite2Import.Business.Models

let fakeSchema : string = "city,country"

type FakeOkSchemaMap() =
    [<CsvColumnAttribute(0)>]
    member val city = "" with get,set
    [<CsvColumnAttribute(1)>]
    member val country = "" with get,set

type FakeWrongOrderSchemaMap() =
    [<CsvColumnAttribute(1)>]
    member val city = "" with get,set
    [<CsvColumnAttribute(0)>]
    member val country = "" with get,set

type FakeWrongMissingSchemaMap() =
    [<CsvColumnAttribute(0)>]
    member val city = "" with get,set

type FakeWrongExtraSchemaMap() =
    [<CsvColumnAttribute(0)>]
    member val city = "" with get,set
    [<CsvColumnAttribute(1)>]
    member val country = "" with get,set
    [<CsvColumnAttribute(2)>]
    member val network = "" with get,set

[<Fact>]
let ``WhenSchemaAttributeMapMatchesSchema_TrueIsReturned`` () =
    Assert.True(IsSchemaValid<FakeOkSchemaMap>(fakeSchema))

[<Fact>]
let ``WhenSchemaAttributeMapHasWrongOrder_FalseIsReturned`` () =
    Assert.False(IsSchemaValid<FakeWrongOrderSchemaMap>(fakeSchema))
    
[<Fact>]
let ``WhenSchemaAttributeMapHasMissingProperty_FalseIsReturned`` () =
    Assert.False(IsSchemaValid<FakeWrongMissingSchemaMap>(fakeSchema))
    
[<Fact>]
let ``WhenSchemaAttributeMapHasExtraProperty_FalseIsReturned`` () =
    Assert.False(IsSchemaValid<FakeWrongExtraSchemaMap>(fakeSchema))