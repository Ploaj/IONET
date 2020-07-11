using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.COMMON
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="technique", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Effect_Technique_COMMON : IONET.Collada.FX.Effects.Effect_Technique
	{
		
		[XmlElement(ElementName = "blinn")]
		public IONET.Collada.FX.Rendering.Blinn Blinn;
		
		[XmlElement(ElementName = "constant")]
		public IONET.Collada.FX.Rendering.Constant Constant;
		
		[XmlElement(ElementName = "lambert")]
		public IONET.Collada.FX.Rendering.Lambert Lambert;
		
		[XmlElement(ElementName = "phong")]
		public IONET.Collada.FX.Rendering.Phong Phong;		
	}
}

