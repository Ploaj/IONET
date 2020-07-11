using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Kinematics_Scenes
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="library_kinematics_scene", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Library_Kinematics_Scene
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
	    
		[XmlElement(ElementName = "kinematics_scene")]
		public IONET.Collada.Kinematics.Kinematics_Scenes.Kinematics_Scene[] Kinematics_Scene;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra; 
	}
}

