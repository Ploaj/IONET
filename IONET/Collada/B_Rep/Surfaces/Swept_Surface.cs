using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Surfaces
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Swept_Surface
	{
		
		[XmlElement(ElementName = "curve")]
		public IONET.Collada.B_Rep.Curves.Curve Curve;
		
		[XmlElement(ElementName = "origin")]
		public IONET.Collada.B_Rep.Transformation.Origin Origin;
		
		[XmlElement(ElementName = "direction")]
		public IONET.Collada.Types.Float_Array_String Direction;

		[XmlElement(ElementName = "axis")]
		public IONET.Collada.Types.Float_Array_String Axis;

		[XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

