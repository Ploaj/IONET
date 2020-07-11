using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="limits", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Constraint_Limits
	{
		
		[XmlElement(ElementName = "swing_cone_and_twist")]
		public IONET.Collada.Physics.Custom_Types.Constraint_Limit_Detail Swing_Cone_And_Twist;		
		
		[XmlElement(ElementName = "linear")]
		public IONET.Collada.Physics.Custom_Types.Constraint_Limit_Detail Linear;		
		

	}
}

