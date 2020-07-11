using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Geometry
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Polylist : IONET.Collada.Core.Geometry.Geometry_Common_Fields
	{
		

		[XmlElement(ElementName = "vcount")]
		public IONET.Collada.Types.Int_Array_String VCount;			
		
		
	}
}

