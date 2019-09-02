namespace Orchestra.Xml

open System
open System.Xml.Serialization

type Repository =
    {
        Metadata : Metadata
        CodeSets : CodeSets
        DataTypes : DataTypes
        Categories : Categories
        Sections : Sections
        Fields : Fields
        Components : Components
        Groups : Groups
        Messages : Messages
    }
