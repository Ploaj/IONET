using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Data_Flow
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Accessor
	{
		[XmlAttribute("count")]
		public uint Count;

		[XmlAttribute("offset")]
		public uint Offset;		
		
		[XmlAttribute("source")]
		public string Source;		
		
		[XmlAttribute("stride")]
		public uint Stride;		
		
	    [XmlElement(ElementName = "param")]
		public IONET.Collada.Core.Parameters.Param[] Param;				
	}
}

