using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="stencil_target", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Stencil_Target
	{
		[XmlAttribute("index")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(int), "1")]
		public int Index;
		
		[XmlAttribute("slice")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(int), "0")]
		public int Slice;

		[XmlAttribute("mip")]
	    [System.ComponentModel.DefaultValueAttribute(typeof(int), "0")]
		public int Mip;


		[XmlAttribute("face")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Face.POSITIVE_X)]
		public IONET.Collada.Enums.Face Face;

		
		[XmlElement(ElementName = "param")]
		public IONET.Collada.Core.Parameters.Param Param;	
		
			
		[XmlElement(ElementName = "instance_image")]
		public IONET.Collada.FX.Texturing.Instance_Image Instance_Image;	
			
	}
}

