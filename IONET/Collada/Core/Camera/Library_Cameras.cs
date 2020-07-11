using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Camera
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Library_Cameras
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
	    [XmlElement(ElementName = "camera")]
		public IONET.Collada.Core.Camera.Camera[] Camera;	
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

