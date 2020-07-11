using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Analytical_Shape
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="box", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Box
	{
		[XmlElement(ElementName = "half_extents")]
		public IONET.Collada.Types.Float_Array_String Half_Extents;		

	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

