using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Geometry
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Spline
	{
		[XmlAttribute("closed")]
		public bool Closed;		
		
		
	    [XmlElement(ElementName = "source")]
		public IONET.Collada.Core.Data_Flow.Source[] Source;		
		
	    [XmlElement(ElementName = "control_vertices")]
		public IONET.Collada.Core.Geometry.Control_Vertices Control_Vertices;		
		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

