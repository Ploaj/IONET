using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class SID_Bool
	{
		[XmlAttribute("sid")]
		public string sID;
		
	    [XmlTextAttribute()]
	    public bool Value;	
	}
}

