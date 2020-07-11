using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Profiles.BRIDGE
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="profile_BRIDGE", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Profile_BRIDGE : IONET.Collada.FX.Profiles.Profile
	{
		[XmlAttribute("platform")]
		public string Platform;
		
		[XmlAttribute("url")]
		public string URL;	
	}
}

