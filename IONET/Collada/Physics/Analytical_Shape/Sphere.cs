using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Analytical_Shape
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="sphere", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Sphere
	{

		[XmlElement(ElementName = "radius")]
		public float Radius;		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

