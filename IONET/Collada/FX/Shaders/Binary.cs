using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Shaders
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="binary", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Binary
	{
		[XmlElement(ElementName = "ref")]
		public string Ref;
		
		[XmlElement(ElementName = "hex")]
		public IONET.Collada.FX.Custom_Types.Hex Hex;		
	}
}

