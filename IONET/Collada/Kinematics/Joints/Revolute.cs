using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Joints
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="revolute", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Revolute
	{
		[XmlAttribute("sid")]
		public string sID;	
		
	    [XmlElement(ElementName = "axis")]
		public IONET.Collada.Types.SID_Float_Array_String Axis;	

		[XmlElement(ElementName = "limits")]
		public IONET.Collada.Kinematics.Custom_Types.Kinematics_Limits Limits;		
	}
}

