using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLSL
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="profile_GLSL", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Profile_GLSL : IONET.Collada.FX.Profiles.Profile
	{
		[XmlAttribute("platform")]
		public string Platform;

	    [XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;

		[XmlElement(ElementName = "technique")]
		public IONET.Collada.FX.Profiles.GLSL.Technique_GLSL[] Technique;	
				
	    [XmlElement(ElementName = "code")]
		public IONET.Collada.FX.Shaders.Code[] Code;
				
	    [XmlElement(ElementName = "include")]
		public IONET.Collada.FX.Shaders.Include[] Include;	
	}
}

