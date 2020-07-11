using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Controller
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Vertex_Weights
	{
		[XmlAttribute("count")]
		public int Count;

        [XmlElement(ElementName = "input")]
        public IONET.Collada.Core.Data_Flow.Input_Shared[] Input;

        [XmlElement(ElementName = "vcount")]
		public IONET.Collada.Types.Int_Array_String VCount;		

		[XmlElement(ElementName = "v")]
		public IONET.Collada.Types.Int_Array_String V;			
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
	}
}

