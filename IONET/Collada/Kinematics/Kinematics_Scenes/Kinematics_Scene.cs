using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Kinematics_Scenes
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="kinematics_scene", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Kinematics_Scene
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
		[XmlElement(ElementName = "instance_kinematics_model")]
		public IONET.Collada.Kinematics.Kinematics_Models.Instance_Kinematics_Model[] Instance_Kinematics_Model;
	    
		[XmlElement(ElementName = "instance_articulated_system")]
		public IONET.Collada.Kinematics.Articulated_Systems.Instance_Articulated_System[] Instance_Articulated_System;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra; 
	}
}

