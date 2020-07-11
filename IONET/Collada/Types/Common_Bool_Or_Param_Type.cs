using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Common_Bool_Or_Param_Type : IONET.Collada.Types.Common_Param_Type
	{
		[XmlElement(ElementName = "bool")]
		public bool Bool;	
	}
}

