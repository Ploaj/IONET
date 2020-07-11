using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLSL
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="shader", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Shader_GLSL : IONET.Collada.FX.Shaders.Shader
	{
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
	}
}

