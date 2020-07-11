using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Lighting
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Library_Lights
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
	    [XmlElement(ElementName = "light")]
		public IONET.Collada.Core.Lighting.Light[] Light;	
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

