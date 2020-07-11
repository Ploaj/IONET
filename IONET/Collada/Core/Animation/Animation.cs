using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Animation
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Animation
	{

		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;			
		
				
		[XmlElement(ElementName = "animation")]
		public IONET.Collada.Core.Animation.Animation[] Animations;
		
		[XmlElement(ElementName = "channel")]
		public IONET.Collada.Core.Animation.Channel[] Channel;
		
		[XmlElement(ElementName = "source")]
		public IONET.Collada.Core.Data_Flow.Source[] Source;

		[XmlElement(ElementName = "sampler")]
		public IONET.Collada.Core.Animation.Sampler[] Sampler;
		
		
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
		
	}
}

