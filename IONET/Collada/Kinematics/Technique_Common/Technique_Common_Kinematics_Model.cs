using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="technique_common", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Technique_Common_Kinematics_Model : IONET.Collada.Core.Extensibility.Technique_Common
	{
		[XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;	

		[XmlElement(ElementName = "joint")]
		public IONET.Collada.Kinematics.Joints.Joint[] Joint;	

		[XmlElement(ElementName = "instance_joint")]
		public IONET.Collada.Kinematics.Kinematics_Models.Instance_Joint[] Instance_Joint;	

		[XmlElement(ElementName = "link")]
		public IONET.Collada.Kinematics.Kinematics_Models.Link[] Link;	

		[XmlElement(ElementName = "formula")]
		public IONET.Collada.Core.Mathematics.Formula[] Formula;	
		
		[XmlElement(ElementName = "instance_formula")]
		public IONET.Collada.Core.Mathematics.Instance_Formula[] Instance_Formula;	
		
	}
}

