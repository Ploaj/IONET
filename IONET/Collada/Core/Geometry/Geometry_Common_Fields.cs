using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Geometry
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Geometry_Common_Fields
	{
		[XmlAttribute("count")]
		public int Count;
		
		[XmlAttribute("name")]
		public string Name;
		
		[XmlAttribute("material")]
		public string Material;

		[XmlElement(ElementName = "input")]
		public IONET.Collada.Core.Data_Flow.Input_Shared[] Input;

        [XmlElement(ElementName = "p")]
        public IONET.Collada.Types.Int_Array_String P;

        [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
		
	}
}

