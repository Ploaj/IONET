using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="spring", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Constraint_Spring
	{
		
		[XmlElement(ElementName = "linear")]
		public IONET.Collada.Physics.Custom_Types.Constraint_Spring_Type Linear;	
		
		[XmlElement(ElementName = "angular")]
		public IONET.Collada.Physics.Custom_Types.Constraint_Spring_Type Angular;			
		
	}
}

