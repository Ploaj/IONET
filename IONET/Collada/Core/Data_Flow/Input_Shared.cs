using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Data_Flow
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Input_Shared : IONET.Collada.Core.Data_Flow.Input_Unshared
	{
		[XmlAttribute("offset")]
		public int Offset;
		
		[XmlAttribute("set")]
		public int Set;				
		
	}
}

