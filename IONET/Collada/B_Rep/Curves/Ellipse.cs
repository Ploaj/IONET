using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Curves
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Ellipse
	{
	    [XmlElement(ElementName = "radius")]
		public IONET.Collada.Types.Float_Array_String Radius;

		[XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;

	}
}

