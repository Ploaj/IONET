using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]

	public partial class Bool_Array_String
	{

		//TODO: cleanup to legit array
		[XmlTextAttribute()]
	    public string Value_As_String;
	}
}


