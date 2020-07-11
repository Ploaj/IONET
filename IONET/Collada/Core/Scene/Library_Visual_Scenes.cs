using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Scene
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Library_Visual_Scenes
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
		
	    [XmlElement(ElementName = "visual_scene")]
		public IONET.Collada.Core.Scene.Visual_Scene[] Visual_Scene;	
		
	}
}

