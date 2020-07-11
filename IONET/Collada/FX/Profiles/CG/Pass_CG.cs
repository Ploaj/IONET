using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.CG
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="pass", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Pass_CG : IONET.Collada.FX.Rendering.Pass
	{
		
	    [XmlElement(ElementName = "program")]
		public IONET.Collada.FX.Profiles.CG.Program_CG Program;
	}
}

