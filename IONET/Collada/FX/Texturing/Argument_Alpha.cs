using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="argument", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Argument_Alpha
	{
		[XmlAttribute("source")]
		public IONET.Collada.Enums.Argument_Source Source;

		[XmlAttribute("operand")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Argument_Alpha_Operand.SRC_ALPHA)]
		public IONET.Collada.Enums.Argument_Alpha_Operand Operand;
				
		[XmlAttribute("sampler")]
		public string Sampler;
		
	}
}

