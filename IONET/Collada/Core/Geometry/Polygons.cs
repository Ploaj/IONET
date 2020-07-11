using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Geometry
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Polygons : IONET.Collada.Core.Geometry.Geometry_Common_Fields
	{

		[XmlElement(ElementName = "ph")]
		public IONET.Collada.Core.Custom_Types.Poly_PH[] PH;		
	
	}
}

