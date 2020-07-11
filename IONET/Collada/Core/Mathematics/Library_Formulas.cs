using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Mathematics
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Library_Formulas
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
	    [XmlElement(ElementName = "formula")]
		public IONET.Collada.Core.Mathematics.Formula[] Formula;	
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

