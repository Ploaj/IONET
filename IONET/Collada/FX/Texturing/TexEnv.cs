using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="texenv", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class TexEnv
	{
		[XmlAttribute("operator")]
		public IONET.Collada.Enums.TexEnv_Operator Operator;

		[XmlAttribute("sampler")]
		public string Sampler;		
		
		[XmlElement(ElementName = "constant")]
		public IONET.Collada.FX.Custom_Types.Constant_Attribute Constant;		
	}
}

