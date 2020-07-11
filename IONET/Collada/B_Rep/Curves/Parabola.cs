using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Curves
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Parabola
	{
	    [XmlElement(ElementName = "focal")]
		public float Focal;

		[XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

