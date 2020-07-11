using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Scene
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Evaluate_Scene
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		[XmlAttribute("sid")]
		public string sid;
		
		[XmlAttribute("enable")]
		public bool Enable;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
		
	    [XmlElement(ElementName = "render")]
		public IONET.Collada.FX.Rendering.Render[] Render;				
		
	}
}

