using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Parameters
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Set_Param
	{
		[XmlAttribute("ref")]
		public string Ref;
		
		
		/// <summary>
		/// The element is the type and the element text is the value or space delimited list of values
		/// </summary>
		[XmlAnyElement]
		public XmlElement[] Data;	
	}
}

