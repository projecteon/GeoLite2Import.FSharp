module GeoLite2Import.Business.CsvAttributeStringParser

open System.Reflection
open GeoLite2Import.Business.Models
open GeoLite2Import.Business.CsvSchemaValidator

let Parse<'a when 'a: (new: unit -> 'a)>(csvString: string): 'a =
    let columns = csvString.Split(',')
    let newObject = new 'a()
    let props = GetSortedCsvColumnAttribute<'a>()
    for prop in props do
        let index = prop.Attribute.ColumnIndex;
        if index >= 0 && columns.Length > index then
            prop.Property.SetValue(newObject, columns.[index]);

    newObject
