using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Technique_Common_Light : IONET.Collada.Core.Extensibility.Technique_Common
	{
		[XmlElement(ElementName = "ambient")]
		public IONET.Collada.Core.Lighting.Ambient Ambient;		
		
		[XmlElement(ElementName = "directional")]
		public IONET.Collada.Core.Lighting.Directional Directional;		
		
		[XmlElement(ElementName = "point")]
		public IONET.Collada.Core.Lighting.Point Point;		
		
		[XmlElement(ElementName = "spot")]
		public IONET.Collada.Core.Lighting.Spot Spot;		
		
		
		
		
		
		
	}
}

