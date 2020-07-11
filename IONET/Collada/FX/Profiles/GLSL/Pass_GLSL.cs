using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLSL
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="pass", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Pass_GLSL : IONET.Collada.FX.Rendering.Pass
	{
		
	    [XmlElement(ElementName = "program")]
		public IONET.Collada.FX.Profiles.GLSL.Program_GLSL Program;
	}
}

