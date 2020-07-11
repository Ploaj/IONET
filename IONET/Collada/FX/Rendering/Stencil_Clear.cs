using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="stencil_clear", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Stencil_Clear
	{
		[XmlAttribute("index")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(int), "0")]
		public int Index;	
	}
}

