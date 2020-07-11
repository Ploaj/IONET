using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Custom_Types
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Axis_Info_Motion : IONET.Collada.Kinematics.Articulated_Systems.Axis_Info
	{
		
		[XmlElement(ElementName = "bind")]
		public IONET.Collada.Kinematics.Articulated_Systems.Bind[] Bind;	

		[XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;	

		[XmlElement(ElementName = "setparam")]
		public IONET.Collada.Core.Parameters.New_Param[] Set_Param;	
		
		[XmlElement(ElementName = "speed")]
		public IONET.Collada.Types.Common_Float_Or_Param_Type Speed;	

		[XmlElement(ElementName = "acceleration")]
		public IONET.Collada.Types.Common_Float_Or_Param_Type Acceleration;	

		[XmlElement(ElementName = "deceleration")]
		public IONET.Collada.Types.Common_Float_Or_Param_Type Deceleration;	

		[XmlElement(ElementName = "jerk")]
		public IONET.Collada.Types.Common_Float_Or_Param_Type Jerk;	

		
		
	}
}

