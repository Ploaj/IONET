using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Analytical_Shape
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="convex_mesh", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Convex_Mesh
	{

		[XmlAttribute("convex_hull_of")]
		public string Convex_Hull_Of;		
		
	    [XmlElement(ElementName = "source")]
		public IONET.Collada.Core.Data_Flow.Source[] Source;		
		
	    [XmlElement(ElementName = "lines")]
		public IONET.Collada.Core.Geometry.Lines[] Lines;		
	    
		[XmlElement(ElementName = "linestrips")]
		public IONET.Collada.Core.Geometry.Linestrips[] Linestrips;		

	    [XmlElement(ElementName = "polygons")]
		public IONET.Collada.Core.Geometry.Polygons[] Polygons;		

	    [XmlElement(ElementName = "polylist")]
		public IONET.Collada.Core.Geometry.Polylist[] Polylist;		
		
	    [XmlElement(ElementName = "triangles")]
		public IONET.Collada.Core.Geometry.Triangles[] Triangles;		
		
	    [XmlElement(ElementName = "trifans")]
		public IONET.Collada.Core.Geometry.Trifans[] Trifans;		
		
	    [XmlElement(ElementName = "tristrips")]
		public IONET.Collada.Core.Geometry.Tristrips[] Tristrips;
		
		
	    [XmlElement(ElementName = "vertices")]
		public IONET.Collada.Core.Geometry.Vertices Vertices;		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

