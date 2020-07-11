using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Controller
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Morph
	{

		[XmlAttribute("source")]
		public string Source_Attribute;
		
		[XmlAttribute("method")]
		public string Method;			
		
		[XmlArray("targets")]
		public IONET.Collada.Core.Data_Flow.Input_Shared[] Targets;		
	
	    [XmlElement(ElementName = "source")]
		public IONET.Collada.Core.Data_Flow.Source[] Source;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
	}
}

