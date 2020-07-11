using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Effects
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="library_effects", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Library_Effects
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;		
		
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
		
	    [XmlElement(ElementName = "effect")]
		public IONET.Collada.FX.Effects.Effect[] Effect;			
		
	}
}

