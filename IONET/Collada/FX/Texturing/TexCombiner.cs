using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="texcombiner", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class TexCombiner
	{
		
		[XmlElement(ElementName = "constant")]
		public IONET.Collada.FX.Custom_Types.Constant_Attribute Constant;		
		
		[XmlElement(ElementName = "RGB")]
		public IONET.Collada.FX.Texturing.RGB RGB;		
		
		[XmlElement(ElementName = "alpha")]
		public IONET.Collada.FX.Texturing.Alpha Alpha;
	}
}

