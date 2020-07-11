using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="format", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Format
	{
		[XmlElement(ElementName = "hint")]
		public IONET.Collada.FX.Custom_Types.Format_Hint Hint;	
		
		[XmlElement(ElementName = "exact")]
		public string Exact;	
	}
}

