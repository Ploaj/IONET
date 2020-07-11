using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLES2
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="pass", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Pass_GLES2 : IONET.Collada.FX.Rendering.Pass
	{
		
	    [XmlElement(ElementName = "program")]
		public IONET.Collada.FX.Profiles.GLES2.Program_GLES2 Program;
	}
}

