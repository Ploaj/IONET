using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Joints
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="library_joints", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Library_Joints
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
	    
		[XmlElement(ElementName = "joint")]
		public IONET.Collada.Kinematics.Joints.Joint[] Joint;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra; 
	}
}

