using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Animation
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Library_Animation_Clips
	{

		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
	    [XmlElement(ElementName = "animation_clip")]
		public IONET.Collada.Core.Animation.Animation_Clip[] Animation_Clip;	
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
	}
}

