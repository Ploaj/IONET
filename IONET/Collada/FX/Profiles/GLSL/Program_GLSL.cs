using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLSL
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="program", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Program_GLSL
	{

	    [XmlElement(ElementName = "shader")]
		public IONET.Collada.FX.Profiles.GLSL.Shader_GLSL[] Shader;	
		
	    [XmlElement(ElementName = "bind_attribute")]
		public IONET.Collada.FX.Shaders.Bind_Attribute[] Bind_Attribute;			

	    [XmlElement(ElementName = "bind_uniform")]
		public IONET.Collada.FX.Shaders.Bind_Uniform[] Bind_Uniform;	
	}
}

