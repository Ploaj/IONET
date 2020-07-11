using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="technique_common", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Technique_Common_Instance_Rigid_Body : IONET.Collada.Core.Extensibility.Technique_Common
	{

		[XmlElement(ElementName = "angular_velocity")]
		public IONET.Collada.Types.Float_Array_String Angular_Velocity;

		[XmlElement(ElementName = "velocity")]
		public IONET.Collada.Types.Float_Array_String Velocity;
		
		[XmlElement(ElementName = "dynamic")]
		public IONET.Collada.Types.SID_Bool Dynamic;
		
		[XmlElement(ElementName = "mass")]
		public IONET.Collada.Types.SID_Float Mass;		
		
		[XmlElement(ElementName = "inertia")]
		public IONET.Collada.Types.SID_Float_Array_String Inertia;		
		
		[XmlElement(ElementName = "mass_frame")]
		public IONET.Collada.Physics.Custom_Types.Mass_Frame Mass_Frame;		
		
		
		[XmlElement(ElementName = "physics_material")]
		public IONET.Collada.Physics.Physics_Material.Physics_Material Physics_Material;		
		
		[XmlElement(ElementName = "instance_physics_material")]
		public IONET.Collada.Physics.Physics_Material.Instance_Physics_Material Instance_Physics_Material;		
		
		
		[XmlElement(ElementName = "shape")]
		public IONET.Collada.Physics.Analytical_Shape.Shape[] Shape;		
	}
}

