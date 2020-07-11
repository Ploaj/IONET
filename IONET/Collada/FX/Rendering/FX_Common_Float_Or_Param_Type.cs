using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="fx_common_float_or_param_type", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class FX_Common_Float_Or_Param_Type
	{
		
		[XmlElement(ElementName = "float")]
		public IONET.Collada.Types.SID_Float Float;	
		
		[XmlElement(ElementName = "param")]
		public IONET.Collada.Core.Parameters.Param Param;	
	}
}

