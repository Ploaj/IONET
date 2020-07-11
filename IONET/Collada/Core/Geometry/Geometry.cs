using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Geometry
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Geometry
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
		[XmlElement(ElementName = "brep")]
		public IONET.Collada.B_Rep.Geometry.B_Rep B_Rep;

		[XmlElement(ElementName = "convex_mesh")]
		public IONET.Collada.Physics.Analytical_Shape.Convex_Mesh Convex_Mesh;

		
		[XmlElement(ElementName = "spline")]
		public IONET.Collada.Core.Geometry.Spline Spline;

		[XmlElement(ElementName = "mesh")]
		public IONET.Collada.Core.Geometry.Mesh Mesh;
		
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
	}
}

