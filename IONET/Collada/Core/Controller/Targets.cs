using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Controller
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Targets
	{

	    [XmlElement(ElementName = "input")]
		public IONET.Collada.Core.Data_Flow.Input_Unshared[] Input;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

