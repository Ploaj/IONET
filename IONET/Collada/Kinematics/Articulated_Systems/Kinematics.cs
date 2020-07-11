using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Articulated_Systems
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="kinematics", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Kinematics
	{
		
		[XmlElement(ElementName = "instance_kinematics_model")]
		public IONET.Collada.Kinematics.Kinematics_Models.Instance_Kinematics_Model[] Instance_Kinematics_Model;

		
		[XmlElement(ElementName = "technique_common")]
		public IONET.Collada.Kinematics.Technique_Common.Technique_Common_Kinematics Technique_Common;
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;			
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

