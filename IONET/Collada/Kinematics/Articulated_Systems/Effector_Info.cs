using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Articulated_Systems
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="effector_info", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Effector_Info
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		[XmlElement(ElementName = "bind")]
		public IONET.Collada.Kinematics.Articulated_Systems.Bind[] Bind;			
		
	    [XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;

		[XmlElement(ElementName = "setparam")]
		public IONET.Collada.Core.Parameters.Set_Param[] Set_Param;			
		
		[XmlElement(ElementName = "speed")]
		public IONET.Collada.Types.Common_Float2_Or_Param_Type Speed;			
		
		[XmlElement(ElementName = "acceleration")]
		public IONET.Collada.Types.Common_Float2_Or_Param_Type Acceleration;			

		[XmlElement(ElementName = "deceleration")]
		public IONET.Collada.Types.Common_Float2_Or_Param_Type Deceleration;			

		[XmlElement(ElementName = "jerk")]
		public IONET.Collada.Types.Common_Float2_Or_Param_Type Jerk;			
		
	}
}

