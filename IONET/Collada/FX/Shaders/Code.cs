using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Shaders
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="code", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Code
	{
		[XmlAttribute("sid")]
		public string sID;

		[XmlTextAttribute()]
	    public string Value;
	}
}

