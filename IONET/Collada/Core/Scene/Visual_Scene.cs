using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Scene
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Visual_Scene
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
		
	    [XmlElement(ElementName = "evaluate_scene")]
		public IONET.Collada.Core.Scene.Evaluate_Scene[] Evaluate_Scene;			

		
	    [XmlElement(ElementName = "node")]
		public IONET.Collada.Core.Scene.Node[] Node;			
		
	}
}

