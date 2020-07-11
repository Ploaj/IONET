using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Scene
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Scene
	{
		
		[XmlElement(ElementName = "instance_visual_scene")]
		public IONET.Collada.Core.Scene.Instance_Visual_Scene Visual_Scene;
			
		[XmlElement(ElementName = "instance_physics_scene")]
		public IONET.Collada.Physics.Physics_Scene.Instance_Physics_Scene[] Physics_Scene;

		[XmlElement(ElementName = "instance_kinematics_scene")]
		public IONET.Collada.Kinematics.Kinematics_Scenes.Instance_Kinematics_Scene Kinematics_Scene;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

