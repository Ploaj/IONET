using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="sampler2D", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Sampler2D : IONET.Collada.FX.Texturing.FX_Sampler_Common
	{

	}
}

