using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="annotate", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class RGB
	{
		[XmlAttribute("operator")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.RGB_Operator.ADD)]
		public IONET.Collada.Enums.RGB_Operator Operator;	

		[XmlAttribute("scale")]
		public float Scale;	
		
	    [XmlElement(ElementName = "argument")]
		public IONET.Collada.FX.Texturing.Argument_RGB[] Argument;			
	}
}

