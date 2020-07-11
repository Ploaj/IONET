using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Curves
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Circle
	{
	    [XmlElement(ElementName = "radius")]
		public float Radius;

		[XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

