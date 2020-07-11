using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Shaders
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="compiler", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Compiler
	{
		[XmlAttribute("platform")]
		public string Platform;

		[XmlAttribute("target")]
		public string Target;
	
		[XmlAttribute("options")]
		public string Options;
	
		[XmlElement(ElementName = "binary")]
		public IONET.Collada.FX.Shaders.Binary Binary;		
	}
}

