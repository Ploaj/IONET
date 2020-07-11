using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Common_Param_Type
	{
		[XmlElement(ElementName = "param")]
		public IONET.Collada.Core.Parameters.Param Param;	
	}
}

