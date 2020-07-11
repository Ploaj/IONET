using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Surfaces
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Nurbs_Surface
	{
		[XmlAttribute("degree_u")]
		public int Degree_U;
		[XmlAttribute("closed_u")]
		public bool Closed_U;		
		[XmlAttribute("degree_v")]
		public int Degree_V;
		[XmlAttribute("closed_v")]
		public bool Closed_V;		
		
		[XmlElement(ElementName = "source")]
		public IONET.Collada.Core.Data_Flow.Source[] Source;		
		
		[XmlElement(ElementName = "control_vertices")]
		public IONET.Collada.Core.Geometry.Control_Vertices Control_Vertices;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
	}
}

