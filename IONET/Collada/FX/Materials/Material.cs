using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Materials
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="material", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Material
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;			
		
				
		[XmlElement(ElementName = "instance_effect")]
		public IONET.Collada.FX.Effects.Instance_Effect Instance_Effect;
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
		
	}
}

