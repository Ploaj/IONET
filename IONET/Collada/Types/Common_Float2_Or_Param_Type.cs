using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Common_Float2_Or_Param_Type
	{
		[XmlElement(ElementName = "param")]
		public IONET.Collada.Core.Parameters.Param Param;	
		//TODO: cleanup to legit array

		[XmlTextAttribute()]
	    public string Value_As_String;
	}
}



