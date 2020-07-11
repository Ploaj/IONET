using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Kinematics_Models
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="attachment_full", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Attachment_Full
	{
		[XmlAttribute("joint")]
		public string Joint;
		
		[XmlElement(ElementName = "translate")]
		public IONET.Collada.Core.Transform.Translate[] Translate;

		[XmlElement(ElementName = "rotate")]
		public IONET.Collada.Core.Transform.Rotate[] Rotate;		

		[XmlElement(ElementName = "link")]
		public IONET.Collada.Kinematics.Kinematics_Models.Link Link;		
		
	}
}

