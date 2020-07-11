using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Shaders
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="shader", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Shader
	{
		[XmlAttribute("stage")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Shader_Stage.VERTEX)]
		public IONET.Collada.Enums.Shader_Stage Stage;		

	    [XmlElement(ElementName = "sources")]
		public IONET.Collada.FX.Shaders.Shader_Sources Sources;	



	}
}

