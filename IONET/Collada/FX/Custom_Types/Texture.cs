using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="texture", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Texture
	{
		[XmlAttribute("texture")]
		public string Textures;
		
		[XmlAttribute("texcoord")]
		public string TexCoord;

	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
	}
}

