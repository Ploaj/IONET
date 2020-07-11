using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLES2
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="program", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Program_GLES2
	{

	    [XmlElement(ElementName = "linker")]
		public IONET.Collada.FX.Shaders.Linker[] Linker;
		
	    [XmlElement(ElementName = "shader")]
		public IONET.Collada.FX.Profiles.GLES2.Shader_GLES2[] Shader;	
		
	    [XmlElement(ElementName = "bind_attribute")]
		public IONET.Collada.FX.Shaders.Bind_Attribute[] Bind_Attribute;			

	    [XmlElement(ElementName = "bind_uniform")]
		public IONET.Collada.FX.Shaders.Bind_Uniform[] Bind_Uniform;	
	}
}

