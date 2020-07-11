using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="color_clear", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Color_Clear : IONET.Collada.Types.Float_Array_String
	{
		[XmlAttribute("index")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(int), "0")]
		public int Index;

	}
}

