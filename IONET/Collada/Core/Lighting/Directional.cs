using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Lighting
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Directional
	{
	    [XmlElement(ElementName = "color")]
		public IONET.Collada.Core.Lighting.Color Color;		

	}
}

