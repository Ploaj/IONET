using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Kinematics_Scenes
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="instance_kinematics_scene", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Instance_Kinematics_Scene
	{
		[XmlAttribute("sid")]
		public string sID;

		[XmlAttribute("name")]
		public string Name;

		[XmlAttribute("url")]
		public string URL;	
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
		
	    [XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;

		[XmlElement(ElementName = "setparam")]
		public IONET.Collada.Core.Parameters.Set_Param[] Set_Param;
		
		
		[XmlElement(ElementName = "bind_kinematics_model")]
		public IONET.Collada.Kinematics.Kinematics_Scenes.Bind_Kinematics_Model[] Bind_Kenematics_Model;

		[XmlElement(ElementName = "bind_joint_axis")]
		public IONET.Collada.Kinematics.Kinematics_Scenes.Bind_Joint_Axis[] Bind_Joint_Axis;
	}
}

