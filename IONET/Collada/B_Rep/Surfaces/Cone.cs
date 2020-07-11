using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Surfaces
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Cone
	{
	    [XmlElement(ElementName = "radius")]
		public float Radius;

	    [XmlElement(ElementName = "angle")]
		public float Angle;
		
		[XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

