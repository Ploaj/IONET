using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Ref_String
	{
		[XmlAttribute("ref")]
		public string Ref;
	}
}

