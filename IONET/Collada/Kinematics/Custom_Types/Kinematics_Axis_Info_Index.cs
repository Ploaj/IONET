using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="index", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Kinematics_Axis_Info_Index : IONET.Collada.Types.Common_Int_Or_Param_Type
	{
		
		[XmlAttribute("semantic")]
		public string Semantic;

		
	}
}

