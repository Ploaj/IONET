using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Articulated_Systems
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="motion", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Motion
	{

		
		[XmlElement(ElementName = "instance_articulated_system")]
		public IONET.Collada.Kinematics.Articulated_Systems.Instance_Articulated_System Instance_Articulated_System;
		
		[XmlElement(ElementName = "technique_common")]
		public IONET.Collada.Kinematics.Technique_Common.Technique_Common_Motion Technique_Common;
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;			
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra; 
	}
}

