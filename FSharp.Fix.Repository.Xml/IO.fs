[<AutoOpen>]
module Fix.Repository.Xml.IO

open System.IO
open System.Xml.Serialization
open Fix.Repository.Xml

let loadDataTypes versionPath =
    use stream = new FileStream(Path.Combine(versionPath, "Base", "Datatypes.xml"), FileMode.Open)
    let serializer = XmlSerializer(typeof<DataTypes>)
    let data = serializer.Deserialize(stream) :?> DataTypes
    data.Items
  
let loadComponents versionPath =
    use stream = new FileStream(Path.Combine(versionPath, "Base", "Components.xml"), FileMode.Open)
    let serializer = XmlSerializer(typeof<Components>)
    let data = serializer.Deserialize(stream) :?> Components
    data.Items

let loadEnums versionPath =
    use stream = new FileStream(Path.Combine(versionPath, "Base", "Enums.xml"), FileMode.Open)
    let serializer = XmlSerializer(typeof<Enums>)
    let data = serializer.Deserialize(stream) :?> Enums
    data.Items

let loadFields versionPath =
    use stream = new FileStream(Path.Combine(versionPath, "Base", "Fields.xml"), FileMode.Open)
    let serializer = XmlSerializer(typeof<Fields>)
    let data = serializer.Deserialize(stream) :?> Fields
    data.Items

let loadMessages versionPath =
    use stream = new FileStream(Path.Combine(versionPath, "Base", "Messages.xml"), FileMode.Open)
    let serializer = XmlSerializer(typeof<Messages>)
    let data = serializer.Deserialize(stream) :?> Messages
    data.Items

let loadMsgContents versionPath =
    use stream = new FileStream(Path.Combine(versionPath, "Base", "MsgContents.xml"), FileMode.Open)
    let serializer = XmlSerializer(typeof<MsgContents>)
    let data = serializer.Deserialize(stream) :?> MsgContents
    data.Items

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
