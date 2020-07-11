using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.COMMON
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="profile_COMMON", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Profile_COMMON : IONET.Collada.FX.Profiles.Profile
	{

		[XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;

		[XmlElement(ElementName = "technique")]
		public IONET.Collada.FX.Profiles.COMMON.Effect_Technique_COMMON Technique;			
	}
}

