namespace Orchestra.Xml

open System.IO
open System.Xml
open System.Xml.Serialization

[<AutoOpen>]
module Serialisation = 

    let loadOrchestraFragment<'OrchestraType> text =
        use streamReader = new StringReader(text)
        let root = XmlRootAttribute()
        root.Namespace <- "http://fixprotocol.io/2016/fixrepository"
        let serializer = XmlSerializer(typeof<'OrchestraType>, root)
        let settings = XmlReaderSettings()
        settings.ConformanceLevel <- ConformanceLevel.Fragment
        let reader = XmlReader.Create(streamReader, settings)
        serializer.Deserialize(reader) :?> 'OrchestraType