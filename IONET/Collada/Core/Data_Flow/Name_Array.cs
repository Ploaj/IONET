using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Data_Flow
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Name_Array : IONET.Collada.Types.String_Array_String
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;			
		
		[XmlAttribute("count")]
		public int Count;			
	}
}

