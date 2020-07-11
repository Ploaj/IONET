using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Materials
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="bind", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Bind_FX
	{
		[XmlAttribute("semantic")]
		public string Semantic;
		
		[XmlAttribute("target")]
		public string Target;		
	}
}

