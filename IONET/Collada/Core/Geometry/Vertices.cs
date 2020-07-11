using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Geometry
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Vertices
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
		[XmlElement(ElementName = "input")]
		public IONET.Collada.Core.Data_Flow.Input_Unshared[] Input;			

	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

