module GeoLite2Import.Business.Models

open System
[<AttributeUsage(AttributeTargets.Property, AllowMultiple = true)>]
type CsvColumnAttribute(columnIndex : int) =
    inherit System.Attribute()
    member this.ColumnIndex = columnIndex

type GeoLite2CityBlock() =
    [<CsvColumnAttribute(0)>]
    member val network = "" with get,set
    [<CsvColumnAttribute(1)>]
    member val geoname_id = "" with get,set
    [<CsvColumnAttribute(2)>]
    member val registered_country_geoname_id = "" with get,set
    [<CsvColumnAttribute(3)>]
    member val represented_country_geoname_id = "" with get,set
    [<CsvColumnAttribute(4)>]
    member val is_anonymous_proxy = "" with get,set
    [<CsvColumnAttribute(5)>]
    member val is_satellite_provider = "" with get,set
    [<CsvColumnAttribute(6)>]
    member val postal_code = "" with get,set
    [<CsvColumnAttribute(7)>]
    member val latitude = "" with get,set
    [<CsvColumnAttribute(8)>]
    member val longitude = "" with get,set
    [<CsvColumnAttribute(9)>]
    member val accuracy_radius = "" with get,set

type GeoLite2CountryBlock() =
    [<CsvColumnAttribute(0)>]
    member val network = "" with get,set
    [<CsvColumnAttribute(1)>]
    member val geoname_id = "" with get,set
    [<CsvColumnAttribute(2)>]
    member val registered_country_geoname_id = "" with get,set
    [<CsvColumnAttribute(3)>]
    member val represented_country_geoname_id = "" with get,set
    [<CsvColumnAttribute(4)>]
    member val is_anonymous_proxy = "" with get,set
    [<CsvColumnAttribute(5)>]
    member val is_satellite_provider = "" with get,set

type WorldCity() =
    [<CsvColumnAttribute(0)>]
    member val Country = "" with get,set
    [<CsvColumnAttribute(1)>]
    member val City = "" with get,set
    [<CsvColumnAttribute(2)>]
    member val AccentCity = "" with get,set
    [<CsvColumnAttribute(3)>]
    member val Region = "" with get,set
    [<CsvColumnAttribute(4)>]
    member val Population = "" with get,set
    [<CsvColumnAttribute(5)>]
    member val Latitude = "" with get,set
    [<CsvColumnAttribute(6)>]
    member val Longitude = "" with get,set
    
type GeoLite2CountryLocation() =
    [<CsvColumnAttribute(0)>]
    member val geoname_id = "" with get,set
    [<CsvColumnAttribute(1)>]
    member val locale_code = "" with get,set
    [<CsvColumnAttribute(2)>]
    member val continent_code = "" with get,set
    [<CsvColumnAttribute(3)>]
    member val continent_name = "" with get,set
    [<CsvColumnAttribute(4)>]
    member val country_iso_code = "" with get,set
    [<CsvColumnAttribute(5)>]
    member val country_name = "" with get,set

type GeoLite2CityLocation() =
    [<CsvColumnAttribute(0)>]
    member val geoname_id = "" with get,set
    [<CsvColumnAttribute(1)>]
    member val locale_code = "" with get,set
    [<CsvColumnAttribute(2)>]
    member val continent_code = "" with get,set
    [<CsvColumnAttribute(3)>]
    member val continent_name = "" with get,set
    [<CsvColumnAttribute(4)>]
    member val country_iso_code = "" with get,set
    [<CsvColumnAttribute(5)>]
    member val country_name = "" with get,set
    [<CsvColumnAttribute(6)>]
    member val subdivision_1_iso_code = "" with get,set            
    [<CsvColumnAttribute(7)>]
    member val subdivision_1_name = "" with get,set
    [<CsvColumnAttribute(8)>]
    member val subdivision_2_iso_code = "" with get,set
    [<CsvColumnAttribute(9)>]
    member val subdivision_2_name = "" with get,set
    [<CsvColumnAttribute(10)>]
    member val city_name = "" with get,set
    [<CsvColumnAttribute(11)>]
    member val metro_code = "" with get,set
    [<CsvColumnAttribute(12)>]
    member val time_zone = "" with get,set
    
    