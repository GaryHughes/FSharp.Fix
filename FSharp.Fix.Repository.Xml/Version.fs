namespace Fix.Repository.Xml

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
