module Repository.Xml.Version

open Repository.Xml.DataType
open Repository.Xml.Enum
open Repository.Xml.Field
open Repository.Xml.Component
open Repository.Xml.MsgContent
open Repository.Xml.Message

// This needs to load lazily
type Version =
    {
        Path:string
        DataTypes:DataType[]
        Enums:Enum[]
        Fields:Field[]
        Components:Component[]
        MsgContents:MsgContent[]
        Messages:Message[]
    }

let loadVersion path =
    {
        Path = path
        DataTypes = loadDataTypes path
        Enums = loadEnums path
        Fields = loadFields path
        Components = loadComponents path
        MsgContents = loadMsgContents path
        Messages = loadMessages path
    }
