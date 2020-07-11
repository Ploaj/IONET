using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="library_images", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Library_Images
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;		
		
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
		
	    [XmlElement(ElementName = "image")]
		public IONET.Collada.FX.Texturing.Image[] Image;
	}
}

