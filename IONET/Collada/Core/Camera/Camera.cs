using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Camera
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Camera
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;		

		[XmlElement(ElementName = "optics")]
		public IONET.Collada.Core.Camera.Optics Optics;
		
		[XmlElement(ElementName = "imager")]
		public IONET.Collada.Core.Camera.Imager Imager;
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
	}
}

