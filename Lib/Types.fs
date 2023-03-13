namespace PostMaster.Lib

open System

type FileNameInfo = {
    Id: string
    Date: DateOnly
}

type YamlNameInfo = {
    Id: string
    DateTime: DateTime
}

type YamlSegment = {
    Id: string
    Value: string
}
