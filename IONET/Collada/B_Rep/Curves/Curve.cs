using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Curves
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Curve
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;			
		
		[XmlElement(ElementName = "line")]
		public IONET.Collada.B_Rep.Curves.Line Line;
		
		[XmlElement(ElementName = "circle")]
		public IONET.Collada.B_Rep.Curves.Circle Circle;
		
		[XmlElement(ElementName = "ellipse")]
		public IONET.Collada.B_Rep.Curves.Ellipse Ellipse;
		
		[XmlElement(ElementName = "parabola")]
		public IONET.Collada.B_Rep.Curves.Parabola Parabola;
		
		[XmlElement(ElementName = "hyperbola")]
		public IONET.Collada.B_Rep.Curves.Hyperbola Hyperbola;
		
		[XmlElement(ElementName = "nurbs")]
		public IONET.Collada.B_Rep.Curves.Nurbs Nurbs;
		
		
	    [XmlElement(ElementName = "orient")]
		public IONET.Collada.B_Rep.Transformation.Orient[] Orient;			
		
		[XmlElement(ElementName = "origin")]
		public IONET.Collada.B_Rep.Transformation.Origin Origin;
	}
}

