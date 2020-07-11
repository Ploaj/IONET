using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Parameters
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="modifier", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Modifier
	{
		[XmlTextAttribute()]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Modifier_Value.CONST)]
	    public IONET.Collada.Enums.Modifier_Value Value;
	}
}

