using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="init_from", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Init_From
	{
		[XmlAttribute("mips_generate")]
        [System.ComponentModel.DefaultValue(false)]
        public bool Mips_Generate;
		
		[XmlAttribute("array_index")]
        [System.ComponentModel.DefaultValue(0)]
        public int Array_Index;
		
		[XmlAttribute("mip_index")]
        [System.ComponentModel.DefaultValue(0)]
        public int Mip_Index;
		
		[XmlAttribute("depth")]
        [System.ComponentModel.DefaultValue(0)]
        public int Depth;
		
		[XmlAttribute("face")]
		[System.ComponentModel.DefaultValue(IONET.Collada.Enums.Face.POSITIVE_X)]
		public IONET.Collada.Enums.Face Face;
		
	    [XmlElement(ElementName = "ref")]
		public string Ref;			
		
	    [XmlElement(ElementName = "hex")]
		public IONET.Collada.FX.Custom_Types.Hex Hex;

        [XmlText()]
        public string Value;
    }
}

