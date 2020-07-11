using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class SID_Int_Array_String
	{
		[XmlAttribute("sid")]
		public string sID;

		[XmlTextAttribute()]
	    public string Value_As_String;
	}
}

