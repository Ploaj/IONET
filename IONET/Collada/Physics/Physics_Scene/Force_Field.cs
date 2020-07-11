using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Physics_Scene
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="force_field", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Force_Field
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

