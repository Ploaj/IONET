using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Geometry
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Library_Geometries
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
	    [XmlElement(ElementName = "geometry")]
		public IONET.Collada.Core.Geometry.Geometry[] Geometry;	
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

