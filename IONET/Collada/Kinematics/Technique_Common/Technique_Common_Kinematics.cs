using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="technique_common", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Technique_Common_Kinematics : IONET.Collada.Core.Extensibility.Technique_Common
	{
		[XmlElement(ElementName = "axis_info")]
		public IONET.Collada.Kinematics.Custom_Types.Axis_Info_Kinematics[] Axis_Info;	
		
		[XmlElement(ElementName = "frame_origin")]
		public IONET.Collada.Kinematics.Articulated_Systems.Frame_Origin Frame_Origin;
		
		[XmlElement(ElementName = "frame_tip")]
		public IONET.Collada.Kinematics.Articulated_Systems.Frame_Tip Frame_Tip;
		
		[XmlElement(ElementName = "frame_tcp")]
		public IONET.Collada.Kinematics.Articulated_Systems.Frame_TCP Frame_TCP;
		
		[XmlElement(ElementName = "frame_object")]
		public IONET.Collada.Kinematics.Articulated_Systems.Frame_Object Frame_Object;
		
		
		
	}
}

