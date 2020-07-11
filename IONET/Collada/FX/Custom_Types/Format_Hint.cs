using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Format_Hint
	{
		[XmlAttribute("channels")]
		public IONET.Collada.Enums.Format_Hint_Channels Channels;	
		
		[XmlAttribute("range")]
		public IONET.Collada.Enums.Format_Hint_Range Range;	
		
		[XmlAttribute("precision")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Format_Hint_Precision.DEFAULT)]
		public IONET.Collada.Enums.Format_Hint_Precision Precision;			
		
		[XmlAttribute("space")]
		public string Hint_Space;
		
	}
}

