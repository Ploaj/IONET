using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Joints
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="joint", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Joint
	{
		[XmlAttribute("id")]
		public string ID;

		[XmlAttribute("name")]
		public string Name;

		[XmlAttribute("sid")]
		public string sID;	
		
	    [XmlElement(ElementName = "prismatic")]
		public IONET.Collada.Kinematics.Joints.Prismatic[] Prismatic;		
	    
		[XmlElement(ElementName = "revolute")]
		public IONET.Collada.Kinematics.Joints.Revolute[] Revolute;		
	    
		[XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

