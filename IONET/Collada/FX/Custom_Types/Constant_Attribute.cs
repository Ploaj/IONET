using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Constant_Attribute
	{
		[XmlAttribute("value")]
		public string Value_As_String;		
		//TODO: needs to be 4 float array
				
		[XmlAttribute("param")]
		public string Param_As_String;
		//TODO: needs to be 4 float array
	}
}

