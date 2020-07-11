using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Physics_Scene
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="library_force_fields", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Library_Force_Fields
	{

		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;		
		
	    [XmlElement(ElementName = "force_field")]
		public IONET.Collada.Physics.Physics_Scene.Force_Field[] Force_Field;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
	}
}

