using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="alpha", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Alpha
	{
		[XmlAttribute("operator")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Alpha_Operator.ADD)]		
		public IONET.Collada.Enums.Alpha_Operator Operator;

		[XmlAttribute("scale")]
		public float Scale;		
		
	    [XmlElement(ElementName = "argument")]
		public IONET.Collada.FX.Texturing.Argument_Alpha[] Argument;			
	}
}

