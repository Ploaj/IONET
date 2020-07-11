using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.CG
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="program", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Program_CG
	{

	    [XmlElement(ElementName = "shader")]
		public IONET.Collada.FX.Profiles.CG.Shader_CG[] Shader;	
	}
}

