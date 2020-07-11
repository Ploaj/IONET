using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="limits", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Kinematics_Limits
	{
	    [XmlElement(ElementName = "min")]
		public IONET.Collada.Types.SID_Name_Float Min;	
	    [XmlElement(ElementName = "max")]
		public IONET.Collada.Types.SID_Name_Float Max;			
	}
}

