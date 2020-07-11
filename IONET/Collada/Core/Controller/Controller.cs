using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Controller
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Controller
	{

		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;			

		
		[XmlElement(ElementName = "skin")]
		public IONET.Collada.Core.Controller.Skin Skin;
		
		[XmlElement(ElementName = "morph")]
		public IONET.Collada.Core.Controller.Morph Morph;
		
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
	}
}

