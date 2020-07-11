using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.CG
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="technique", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Technique_CG : IONET.Collada.FX.Effects.Effect_Technique
	{
		
		[XmlElement(ElementName = "annotate")]
		public IONET.Collada.FX.Effects.Annotate[] Annotate;
		
		[XmlElement(ElementName = "pass")]
		public IONET.Collada.FX.Profiles.CG.Pass_CG[] Pass;		
	}
}

