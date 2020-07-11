using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Extensibility
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Extra
	{

		[XmlAttribute("id")]
		public string ID;
		[XmlAttribute("name")]
		public string Name;
		[XmlAttribute("type")]
		public string Type;		
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;		
		
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;		
	}
}

