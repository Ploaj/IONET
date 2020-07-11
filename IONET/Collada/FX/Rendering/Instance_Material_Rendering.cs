using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="instance_material", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Instance_Material_Rendering
	{
		[XmlAttribute("url")]
		public string URL;	
		
	    [XmlElement(ElementName = "technique_override")]
		public IONET.Collada.FX.Custom_Types.Technique_Override Technique_Override;	
		
	    [XmlElement(ElementName = "bind")]
		public IONET.Collada.FX.Materials.Bind_FX[] Bind;	
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

