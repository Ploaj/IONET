using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Constraint_Limit_Detail
	{
		
		[XmlElement(ElementName = "min")]
		public IONET.Collada.Types.SID_Float_Array_String Min;	
		
		
		[XmlElement(ElementName = "max")]
		public IONET.Collada.Types.SID_Float_Array_String Max;			
	}
}

