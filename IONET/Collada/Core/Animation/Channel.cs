using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Animation
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Channel
	{
		[XmlAttribute("source")]
		public string Source;
		
		[XmlAttribute("target")]
		public string Target;		
		
	}
}

