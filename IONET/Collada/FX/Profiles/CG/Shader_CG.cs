using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.CG
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="shader", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Shader_CG : IONET.Collada.FX.Shaders.Shader
	{

	    [XmlElement(ElementName = "bind_uniform")]
		public IONET.Collada.FX.Shaders.Bind_Uniform[] Bind_Uniform;	
		
	    [XmlElement(ElementName = "compiler")]
		public IONET.Collada.FX.Shaders.Compiler[] Compiler;		
	}
}

