using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Physics_Scene
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="physics_scene", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Physics_Scene
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
	    [XmlElement(ElementName = "instance_force_field")]
		public IONET.Collada.Physics.Physics_Scene.Instance_Force_Field[] Instance_Force_Field;		
		
	    [XmlElement(ElementName = "instance_physics_model")]
		public IONET.Collada.Physics.Physics_Model.Instance_Physics_Model[] Instance_Physics_Model;		
		
		[XmlElement(ElementName = "technique_common")]
		public IONET.Collada.Physics.Technique_Common.Technique_Common_Physics_Scene Technique_Common;
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

