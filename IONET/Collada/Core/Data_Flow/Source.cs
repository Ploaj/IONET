using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Data_Flow
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Source
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;			
		

		[XmlElement(ElementName = "bool_array")]
		public IONET.Collada.Core.Data_Flow.Bool_Array Bool_Array;
		[XmlElement(ElementName = "float_array")]
		public IONET.Collada.Core.Data_Flow.Float_Array Float_Array;
		[XmlElement(ElementName = "IDREF_array")]
		public IONET.Collada.Core.Data_Flow.IDREF_Array IDREF_Array;
		[XmlElement(ElementName = "int_array")]
		public IONET.Collada.Core.Data_Flow.Int_Array Int_Array;
		[XmlElement(ElementName = "Name_array")]
		public IONET.Collada.Core.Data_Flow.Name_Array Name_Array;
		[XmlElement(ElementName = "SIDREF_array")]
		public IONET.Collada.Core.Data_Flow.SIDREF_Array SIDREF_Array;
		[XmlElement(ElementName = "token_array")]
		public IONET.Collada.Core.Data_Flow.Token_Array Token_Array;
		
		
		[XmlElement(ElementName = "technique_common")]
		public IONET.Collada.Core.Technique_Common.Technique_Common_Source Technique_Common;
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;	
	}
}

