using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Physics_Model
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="instance_physics_model", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Instance_Physics_Model
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		[XmlAttribute("url")]
		public string URL;	
		
		[XmlAttribute("parent")]
		public string Parent;	
		
	    [XmlElement(ElementName = "instance_force_field")]
		public IONET.Collada.Physics.Physics_Scene.Instance_Force_Field[] Instance_Force_Field;		
		
	    [XmlElement(ElementName = "instance_rigid_body")]
		public IONET.Collada.Physics.Physics_Model.Instance_Rigid_Body[] Instance_Rigid_Body;		

		[XmlElement(ElementName = "instance_rigid_constraint")]
		public IONET.Collada.Physics.Physics_Model.Instance_Rigid_Constraint[] Instance_Rigid_Constraint;		
		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

