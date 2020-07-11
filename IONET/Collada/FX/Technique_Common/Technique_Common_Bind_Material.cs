using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="technique_common", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Technique_Common_Bind_Material : IONET.Collada.Core.Extensibility.Technique_Common
	{
	    
		[XmlElement(ElementName = "instance_material")]
		public IONET.Collada.FX.Materials.Instance_Material_Geometry[] Instance_Material;	
		
	}
}

