using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLES
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="technique", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Technique_GLES : IONET.Collada.FX.Effects.Effect_Technique
	{
		
		[XmlElement(ElementName = "annotate")]
		public IONET.Collada.FX.Effects.Annotate[] Annotate;	
		
		[XmlElement(ElementName = "pass")]
		public IONET.Collada.FX.Profiles.GLES.Pass_GLES[] Pass;		
	}
}

