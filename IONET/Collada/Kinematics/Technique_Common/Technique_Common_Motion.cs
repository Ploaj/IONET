using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="technique_common", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Technique_Common_Motion : IONET.Collada.Core.Extensibility.Technique_Common
	{
		
		[XmlElement(ElementName = "axis_info")]
		public IONET.Collada.Kinematics.Custom_Types.Axis_Info_Motion[] Axis_Info;	
		
		[XmlElement(ElementName = "effector_info")]
		public IONET.Collada.Kinematics.Articulated_Systems.Effector_Info Effector_Info;			
		
	}
}

