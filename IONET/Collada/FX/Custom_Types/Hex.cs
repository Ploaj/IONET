using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Hex
	{
		[XmlAttribute("format")]
		public string Format;
		
		[XmlTextAttribute()]
	    public string Value;	
		//TODO: this is a hex array
	}
}

