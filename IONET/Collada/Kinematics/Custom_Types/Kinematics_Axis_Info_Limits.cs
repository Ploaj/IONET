using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Kinematics_Axis_Info_Limits
	{
	    [XmlElement(ElementName = "min")]
		public IONET.Collada.Types.Common_Float_Or_Param_Type Min;	
	    [XmlElement(ElementName = "max")]
		public IONET.Collada.Types.Common_Float_Or_Param_Type Max;
	}
}

