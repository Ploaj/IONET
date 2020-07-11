using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="fx_common_color_or_texture_type", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class FX_Common_Color_Or_Texture_Type
	{
		[XmlAttribute("opaque")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.FX_Opaque_Channel.A_ONE)]		
		public IONET.Collada.Enums.FX_Opaque_Channel Opaque;

		[XmlElement(ElementName = "param")]
		public IONET.Collada.Core.Parameters.Param Param;			
		
		[XmlElement(ElementName = "color")]
		public IONET.Collada.Core.Lighting.Color Color;			
		
		[XmlElement(ElementName = "texture")]
		public IONET.Collada.FX.Custom_Types.Texture Texture;			
	}
}

