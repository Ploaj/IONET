using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Parameters
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="semantic", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Semantic
	{
		[XmlTextAttribute()]
	    public string Value;
	}
}

