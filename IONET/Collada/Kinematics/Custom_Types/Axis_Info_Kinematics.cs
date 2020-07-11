using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Custom_Types
{
	[Serializable()]
	[XmlType(AnonymousType=true)]

	public partial class Axis_Info_Kinematics : IONET.Collada.Kinematics.Articulated_Systems.Axis_Info
	{
		[XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;	
		
		[XmlElement(ElementName = "active")]
		public IONET.Collada.Types.Common_Bool_Or_Param_Type Active;	
		
		[XmlElement(ElementName = "locked")]
		public IONET.Collada.Types.Common_Bool_Or_Param_Type Locked;	
		
		[XmlElement(ElementName = "index")]
		public IONET.Collada.Kinematics.Custom_Types.Kinematics_Axis_Info_Index[] Index;	
		
		[XmlElement(ElementName = "limits")]
		public IONET.Collada.Kinematics.Custom_Types.Kinematics_Axis_Info_Limits Limits;	
		
		[XmlElement(ElementName = "formula")]
		public IONET.Collada.Core.Mathematics.Formula[] Formula;	
		
		[XmlElement(ElementName = "instance_formula")]
		public IONET.Collada.Core.Mathematics.Instance_Formula[] Instance_Formula;	
	}
}

