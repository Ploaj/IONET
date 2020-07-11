using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Lighting
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Spot
	{
		[XmlElement(ElementName = "color")]
		public IONET.Collada.Core.Lighting.Color Color;				
		
		[XmlElement(ElementName = "constant_attenuation")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(float), "1.0")]
		public IONET.Collada.Types.SID_Float Constant_Attenuation;				
				
		[XmlElement(ElementName = "linear_attenuation")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(float), "0.0")]
		public IONET.Collada.Types.SID_Float Linear_Attenuation;				

		[XmlElement(ElementName = "quadratic_attenuation")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(float), "0.0")]
		public IONET.Collada.Types.SID_Float Quadratic_Attenuation;

		[XmlElement(ElementName = "falloff_angle")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(float), "180.0")]
		public IONET.Collada.Types.SID_Float Falloff_Angle;

		[XmlElement(ElementName = "falloff_exponent")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(float), "0.0")]
		public IONET.Collada.Types.SID_Float Falloff_Exponent;

	
	}
}

