namespace Orchestra.Xml

open System.Xml.Serialization

// <fixr:actors>
//     <fixr:actor name="BuySide">
//         <fixr:annotation supported="supported">
//             <fixr:documentation supported="supported">An order originator that intends to make a profit or mitigate risk</fixr:documentation>
//         </fixr:annotation>
//     </fixr:actor>
//     <fixr:actor name="Market">
//         <fixr:states name="MarketPhase">
//             <fixr:initial name="Closed">
//                 <fixr:transition name="Reopening" target="Preopen"/>
//                 <fixr:onentry>
//                     <fixr:messageRef name="TradingSessionStatus" implMinOccurs="1" implMaxOccurs="unbounded" id="41" scenario="closed"/>
//                 </fixr:onentry>
//                 <fixr:annotation supported="supported">
//                     <fixr:documentation supported="supported">No orders or quotes may be entered.</fixr:documentation>
//                 </fixr:annotation>
//             </fixr:initial>
//             <fixr:state name="Halted">
//                 <fixr:transition name="Resumed" target="Preopen">
//                     <fixr:annotation supported="supported">
//                         <fixr:documentation supported="supported">Authorities resume a market after a halt.</fixr:documentation>
//                     </fixr:annotation>
//                 </fixr:transition>
//                 <fixr:onentry>
//                     <fixr:messageRef name="TradingSessionStatus" implMinOccurs="1" implMaxOccurs="unbounded" id="41" scenario="halted"/>
//                 </fixr:onentry>
//                 <fixr:annotation supported="supported">
//                     <fixr:documentation supported="supported">Order matching is suspended due to unexpected conditions or by a circuit-breaker rule.</fixr:documentation>
//                 </fixr:annotation>
//             </fixr:state>
//             <fixr:annotation><fixr:documentation>OrderState state machine associated to OrdStatus (39) code set and order state matrices</fixr:documentation></fixr:annotation>
//             </fixr:states>
//             <fixr:fieldRef id="75" scenario="base" supported="supported" presence="required"/>
//             <fixr:fieldRef id="1679" scenario="base" supported="supported" presence="required"/>
//             <fixr:groupRef implMaxOccurs="unbounded" presence="required" id="2186" scenario="base" supported="supported">
//                 <fixr:annotation supported="supported">
//                     <fixr:documentation supported="supported">Repeating group of security status by SecurityID</fixr:documentation>
//                 </fixr:annotation>
//             </fixr:groupRef>
//             <fixr:annotation supported="supported">
//                 <fixr:documentation supported="supported">Matches orders entered by buy-side participants</fixr:documentation>
//             </fixr:annotation>
//     </fixr:actor>
//     <fixr:flow name="OrderEntry" source="BuySide" destination="Market"/>
//     <fixr:flow name="Executions" source="Market" destination="BuySide"/>
// </fixr:actors>

[<CLIMutable>]
[<XmlType("transition")>]
type Transition =
    {
        [<XmlAttribute("name")>]
        Name:string
        [<XmlAttribute("target")>]
        Target:string
    }

[<CLIMutable>]
[<XmlType("messageRef")>]
type MessageRef =
    {
        [<XmlAttribute("name")>]
        Name:string
        [<XmlAttribute("implMinOccurs")>]
        ImplMinOccurs:string
        [<XmlAttribute("implMaxOccurs")>]
        ImplMaxOccurs:string
        [<XmlAttribute("id")>]
        Id:int 
        [<XmlAttribute("scenario")>]
        Scenario:string
    }

[<CLIMutable>]
[<XmlType("oneEntry")>]
type OneEntry =
    {
        MessageRef:MessageRef
    }

[<CLIMutable>]
[<XmlType("state")>]
type State =
    {
        [<XmlElement("transition")>]
        Transition:Transition
        [<XmlElement("oneEntry")>]
        OneEntry:OneEntry
        [<XmlElement("annotation")>]
        Annotation:Annotation
    }

[<CLIMutable>]
[<XmlType("states")>]
type States =
    {
        [<XmlElement("initial")>]
        Initial:State
        [<XmlElement("state")>]
        State:State[]
        [<XmlElement("annotation")>]
        Annotation:Annotation
    }

[<CLIMutable>]
[<XmlType("actor")>]
type Actor =
    {
        [<XmlElement("states")>]
        States:States
        [<XmlElement("annotation")>]
        Annotation:Annotation[]
    }

[<CLIMutable>]
[<XmlType("flow")>]
type Flow =
    {
        [<XmlAttribute("name")>]
        Name:string
        [<XmlAttribute("source")>]
        Source:string 
        [<XmlAttribute("destination")>]
        Destination:string
    }

[<CLIMutable>]
[<XmlType("actors")>]
type Actors =
    {
        [<XmlElement("actor")>]
        Actors:Actor[]
        [<XmlElement("flow")>]
        Flows:Flow[]
    }
