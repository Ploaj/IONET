using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="evaluate", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Effect_Technique_Evaluate
	{

	    [XmlElement(ElementName = "color_target")]
		public IONET.Collada.FX.Rendering.Color_Target Color_Target;	

		[XmlElement(ElementName = "depth_target")]
		public IONET.Collada.FX.Rendering.Depth_Target Depth_Target;	
	    
		[XmlElement(ElementName = "stencil_target")]
		public IONET.Collada.FX.Rendering.Stencil_Target Stencil_Target;	
	    
		[XmlElement(ElementName = "color_clear")]
		public IONET.Collada.FX.Rendering.Color_Clear Color_Clear;	
	    
		[XmlElement(ElementName = "depth_clear")]
		public IONET.Collada.FX.Rendering.Depth_Clear Depth_Clear;	
	    
		[XmlElement(ElementName = "stencil_clear")]
		public IONET.Collada.FX.Rendering.Stencil_Clear Stencil_Clear;	
	    
		[XmlElement(ElementName = "draw")]
		public IONET.Collada.FX.Rendering.Draw Draw;	

		
	}
}

