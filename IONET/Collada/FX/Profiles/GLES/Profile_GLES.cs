using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.GLES
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="profile_GLES", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Profile_GLES : IONET.Collada.FX.Profiles.Profile
	{
		[XmlAttribute("platform")]
		public string Platform;
				
	    [XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;

		[XmlElement(ElementName = "technique")]
		public IONET.Collada.FX.Profiles.GLES.Technique_GLES[] Technique;			
	}
}

