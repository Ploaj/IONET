using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="texture_pipeline", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Texture_Pipeline
	{
		
		[XmlAttribute("sid")]
		public string sID;		
		
		
	    [XmlElement(ElementName = "texcombiner")]
		public IONET.Collada.FX.Texturing.TexCombiner[] TexCombiner;	
		
	    [XmlElement(ElementName = "texenv")]
		public IONET.Collada.FX.Texturing.TexEnv[] TexEnv;	
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			

	}
}

