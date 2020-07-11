using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Analytical_Shape
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="shape", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Shape
	{
		[XmlElement(ElementName = "hollow")]
		public IONET.Collada.Types.SID_Bool Hollow;				
				
		[XmlElement(ElementName = "mass")]
		public IONET.Collada.Types.SID_Float Mass;			
				
		[XmlElement(ElementName = "density")]
		public IONET.Collada.Types.SID_Float Density;	


		[XmlElement(ElementName = "physics_material")]
		public IONET.Collada.Physics.Physics_Material.Physics_Material Physics_Material;	

		[XmlElement(ElementName = "instance_physics_material")]
		public IONET.Collada.Physics.Physics_Material.Instance_Physics_Material Instance_Physics_Material;	
		
		
		[XmlElement(ElementName = "instance_geometry")]
		public IONET.Collada.Core.Geometry.Instance_Geometry Instance_Geometry;	
		
		[XmlElement(ElementName = "plane")]
		public IONET.Collada.Physics.Analytical_Shape.Plane Plane;	
		[XmlElement(ElementName = "box")]
		public IONET.Collada.Physics.Analytical_Shape.Box Box;	
		[XmlElement(ElementName = "sphere")]
		public IONET.Collada.Physics.Analytical_Shape.Sphere Sphere;	
		[XmlElement(ElementName = "cylinder")]
		public IONET.Collada.Physics.Analytical_Shape.Cylinder Cylinder;	
		[XmlElement(ElementName = "capsule")]
		public IONET.Collada.Physics.Analytical_Shape.Capsule Capsule;	
		
		
		
		[XmlElement(ElementName = "translate")]
		public IONET.Collada.Core.Transform.Translate[] Translate;

		[XmlElement(ElementName = "rotate")]
		public IONET.Collada.Core.Transform.Rotate[] Rotate;		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
	}
}

