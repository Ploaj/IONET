using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Topology
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Edges
	{
		[XmlAttribute("count")]
		public int Count;
		
		[XmlAttribute("name")]
		public string Name;
		
		[XmlAttribute("id")]
		public string ID;

	    [XmlElement(ElementName = "p")]
		public IONET.Collada.Types.Int_Array_String P;		
		
	    [XmlElement(ElementName = "input")]
		public IONET.Collada.Core.Data_Flow.Input_Shared[] Input;		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		

	}
}

