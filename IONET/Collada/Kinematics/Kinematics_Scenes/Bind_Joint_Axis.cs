using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Kinematics_Scenes
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="bind_joint_axis", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Bind_Joint_Axis
	{
		[XmlAttribute("target")]
		public string Target;


		[XmlElement(ElementName = "axis")]
		public IONET.Collada.Types.Common_SIDREF_Or_Param_Type Axis;	
		
		[XmlElement(ElementName = "value")]
		public IONET.Collada.Types.Common_Float_Or_Param_Type Value;	
		

	}
}

