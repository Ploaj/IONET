using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Surfaces
{
	[Serializable()]
	[XmlType(AnonymousType=true)]

	public partial class Cylinder_B_Rep
	{
	    [XmlElement(ElementName = "radius")]
		public IONET.Collada.Types.Float_Array_String Radius;

		[XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

