using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Physics_Model
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="physics_model", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Physics_Model
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;		
		
	    [XmlElement(ElementName = "rigid_body")]
		public IONET.Collada.Physics.Physics_Model.Rigid_Body[] Rigid_Body;			
		
	    [XmlElement(ElementName = "rigid_constraint")]
		public IONET.Collada.Physics.Physics_Model.Rigid_Constraint[] Rigid_Constraint;			
		
	    [XmlElement(ElementName = "instance_physics_model")]
		public IONET.Collada.Physics.Physics_Model.Instance_Physics_Model[] Instance_Physics_Model;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

