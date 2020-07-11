using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="constant", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Constant
	{
		[XmlElement(ElementName = "emission")]
		public IONET.Collada.FX.Rendering.FX_Common_Color_Or_Texture_Type Eission;		
		
		[XmlElement(ElementName = "reflective")]
		public IONET.Collada.FX.Rendering.FX_Common_Color_Or_Texture_Type Reflective;
		
		[XmlElement(ElementName = "reflectivity")]
		public IONET.Collada.FX.Rendering.FX_Common_Float_Or_Param_Type Reflectivity;		
		

		
		[XmlElement(ElementName = "transparent")]
		public IONET.Collada.FX.Rendering.FX_Common_Color_Or_Texture_Type Transparent;
		
		[XmlElement(ElementName = "transparency")]
		public IONET.Collada.FX.Rendering.FX_Common_Float_Or_Param_Type Transparency;			
		
		[XmlElement(ElementName = "index_of_refraction")]
		public IONET.Collada.FX.Rendering.FX_Common_Float_Or_Param_Type Index_Of_Refraction;			
	}
}

