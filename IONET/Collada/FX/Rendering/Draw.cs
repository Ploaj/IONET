using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="draw", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Draw
	{
		[XmlTextAttribute()]
	    public string Value;
	}
}

