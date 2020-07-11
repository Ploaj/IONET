using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Constraint_Spring_Type
	{

		[XmlElement(ElementName = "stiffness")]
		public IONET.Collada.Types.SID_Float Stiffness;	
		
		[XmlElement(ElementName = "damping")]
		public IONET.Collada.Types.SID_Float Damping;	
		
		[XmlElement(ElementName = "target_value")]
		public IONET.Collada.Types.SID_Float Target_Value;			
	}
}

