using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLES
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="pass", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Pass_GLES : IONET.Collada.FX.Rendering.Pass
	{

	}
}

