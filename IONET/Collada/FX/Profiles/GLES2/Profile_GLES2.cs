using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLES2
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="profile_GLES2", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Profile_GLES2 : IONET.Collada.FX.Profiles.Profile
	{
		[XmlAttribute("platform")]
		public string Platform;
		
		[XmlAttribute("language")]
		public string Language;	
				
	    [XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;

		[XmlElement(ElementName = "technique")]
		public IONET.Collada.FX.Profiles.GLES2.Technique_GLES2[] Technique;	
				
	    [XmlElement(ElementName = "code")]
		public IONET.Collada.FX.Shaders.Code[] Code;
				
	    [XmlElement(ElementName = "include")]
		public IONET.Collada.FX.Shaders.Include[] Include;
	}
}

