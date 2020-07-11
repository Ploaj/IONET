using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Articulated_Systems
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="connect_param", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Connect_Param
	{
		[XmlAttribute("ref")]
		public string Ref;
	}
}

