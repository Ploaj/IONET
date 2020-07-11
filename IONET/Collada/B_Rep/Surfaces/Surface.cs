using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Surfaces
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Surface
	{
		[XmlAttribute("name")]
		public string Name;
		
		[XmlAttribute("sid")]
		public string sID;
		

		[XmlElement(ElementName = "cone")]
		public IONET.Collada.B_Rep.Surfaces.Cone Cone;
		
	    [XmlElement(ElementName = "plane")]
		public IONET.Collada.Physics.Analytical_Shape.Plane Plane;
		
	    [XmlElement(ElementName = "cylinder")]
		public IONET.Collada.B_Rep.Surfaces.Cylinder_B_Rep Cylinder;
		
	    [XmlElement(ElementName = "nurbs_surface")]
		public IONET.Collada.B_Rep.Surfaces.Nurbs_Surface Nurbs_Surface;
		
	    [XmlElement(ElementName = "sphere")]
		public IONET.Collada.Physics.Analytical_Shape.Sphere Sphere;
		
	    [XmlElement(ElementName = "torus")]
		public IONET.Collada.B_Rep.Surfaces.Torus Torus;
		
	    [XmlElement(ElementName = "swept_surface")]
		public IONET.Collada.B_Rep.Surfaces.Swept_Surface Swept_Surface;
	

		
		[XmlElement(ElementName = "orient")]
		public IONET.Collada.B_Rep.Transformation.Orient[] Orient;		
		
	    [XmlElement(ElementName = "origin")]
		public IONET.Collada.B_Rep.Transformation.Origin Origin;

	}
}

