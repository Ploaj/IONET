using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="size_ratio", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Size_Ratio
	{
		[XmlAttribute("width")]
		public float Width;	

		[XmlAttribute("height")]
		public float Height;
	}
}

