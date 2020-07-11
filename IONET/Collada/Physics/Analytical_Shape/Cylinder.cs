using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Analytical_Shape
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="cylinder", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Cylinder
	{
		[XmlElement(ElementName = "height")]
		public float Height;		

		[XmlElement(ElementName = "radius")]
		public IONET.Collada.Types.Float_Array_String Radius;		

		[XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
	}
}

