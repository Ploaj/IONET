using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Data_Flow
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Input_Unshared
	{
		[XmlAttribute("semantic")]
		public IONET.Collada.Enums.Input_Semantic Semantic;	

		[XmlAttribute("source")]
		public string source;

	}
}

