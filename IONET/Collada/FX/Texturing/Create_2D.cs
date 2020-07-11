using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="create_2d", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Create_2D
	{
		
		[XmlElement(ElementName = "size_exact")]
		public IONET.Collada.FX.Custom_Types.Size_2D Size_Exact;	
	
		
		[XmlElement(ElementName = "size_ratio")]
		public IONET.Collada.FX.Custom_Types.Size_Ratio Size_Ratio;	
		
		[XmlElement(ElementName = "mips")]
		public IONET.Collada.FX.Custom_Types.Mips_Attribute Mips;	
	
		

		[XmlElement(ElementName = "unnormalized")]
		public XmlElement Unnormalized;	
		
		[XmlElement(ElementName = "array")]
		public IONET.Collada.FX.Custom_Types.Array_Length Array_Length;		
		
		
		[XmlElement(ElementName = "format")]
		public IONET.Collada.FX.Texturing.Format Format;		
		
		[XmlElement(ElementName = "init_from")]
		public IONET.Collada.FX.Texturing.Init_From[] Init_From;		
				
		
	}
}

