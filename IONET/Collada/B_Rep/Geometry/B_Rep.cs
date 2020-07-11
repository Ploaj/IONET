using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Geometry
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class B_Rep
	{
		
				
		[XmlElement(ElementName = "curves")]
		public IONET.Collada.B_Rep.Curves.Curves Curves;
				
		[XmlElement(ElementName = "surface_curves")]
		public IONET.Collada.B_Rep.Curves.Surface_Curves Surface_Curves;
				
		[XmlElement(ElementName = "surfaces")]
		public IONET.Collada.B_Rep.Surfaces.Surfaces Surfaces;
		
		[XmlElement(ElementName = "source")]
		public IONET.Collada.Core.Data_Flow.Source[] Source;
		
		[XmlElement(ElementName = "vertices")]
		public IONET.Collada.Core.Geometry.Vertices Vertices;
		
		
		[XmlElement(ElementName = "edges")]
		public IONET.Collada.B_Rep.Topology.Edges Edges;
		
		[XmlElement(ElementName = "wires")]
		public IONET.Collada.B_Rep.Topology.Wires Wires;
		
		[XmlElement(ElementName = "faces")]
		public IONET.Collada.B_Rep.Topology.Faces Faces;
		
		[XmlElement(ElementName = "pcurves")]
		public IONET.Collada.B_Rep.Topology.PCurves PCurves;
		
		[XmlElement(ElementName = "shells")]
		public IONET.Collada.B_Rep.Topology.Shells Shells;
		
		[XmlElement(ElementName = "solids")]
		public IONET.Collada.B_Rep.Topology.Solids Solids;

		
		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;					
		
	}
}

