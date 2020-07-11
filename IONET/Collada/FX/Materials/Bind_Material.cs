using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Materials
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="bind_material", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Bind_Material
	{
	    
		[XmlElement(ElementName = "param")]
		public IONET.Collada.Core.Parameters.Param[] Param;			

		[XmlElement(ElementName = "technique_common")]
		public IONET.Collada.FX.Technique_Common.Technique_Common_Bind_Material Technique_Common;
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;			
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

