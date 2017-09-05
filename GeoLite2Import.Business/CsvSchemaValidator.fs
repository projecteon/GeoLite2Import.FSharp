module GeoLite2Import.Business.CsvSchemaValidator

open System.Reflection
open GeoLite2Import.Business.Models

type ReflectedCsvColumnAttributeProperty = {Property: PropertyInfo; Attribute: CsvColumnAttribute}

let GetCsvColumnAttribute(propertyInfo: PropertyInfo) =
    let attributes = propertyInfo.GetCustomAttributes(typeof<CsvColumnAttribute>, true)
    if attributes.Length > 0 then
        Some(attributes.[0] :?> CsvColumnAttribute)
    else None

let MapCsvColumnAttribute(propertyInfo: PropertyInfo) =
    let attributes = GetCsvColumnAttribute(propertyInfo)
    if attributes.IsSome then
        Some({Property = propertyInfo; Attribute = attributes.Value})
    else None

let GetSortedCsvColumnAttribute<'a>() =
    let bindingFlags = 
        BindingFlags.Public   ||| 
        BindingFlags.Instance ||| 
        BindingFlags.GetProperty
    let properties = typeof<'a>.GetProperties(bindingFlags)
    properties  |> Seq.choose MapCsvColumnAttribute |> Seq.sortBy (fun f -> f.Attribute.ColumnIndex)

let GetCsvColumnAttributeSchema<'a>() =    
    let csvColumnProperties = GetSortedCsvColumnAttribute<'a>()
    let csvColumnPropertyNames = csvColumnProperties |> Seq.map (fun f -> f.Property.Name)
    csvColumnPropertyNames |> Seq.reduce (fun x y -> x + "," + y)

let IsSchemaValid<'a>(schema: string) =
    schema.Equals(GetCsvColumnAttributeSchema<'a>())
