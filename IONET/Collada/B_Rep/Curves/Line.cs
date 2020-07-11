using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Curves
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Line
	{
		[XmlElement(ElementName = "origin")]
		public IONET.Collada.B_Rep.Transformation.Origin Origin;
		
		[XmlElement(ElementName = "direction")]
		public IONET.Collada.Types.Float_Array_String Direction;
				
		[XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

