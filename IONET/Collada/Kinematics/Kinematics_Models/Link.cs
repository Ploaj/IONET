using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Kinematics_Models
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="link", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Link
	{
		[XmlAttribute("sid")]
		public string sID;
		[XmlAttribute("name")]
		public string Name;
		
		[XmlElement(ElementName = "translate")]
		public IONET.Collada.Core.Transform.Translate[] Translate;

		[XmlElement(ElementName = "rotate")]
		public IONET.Collada.Core.Transform.Rotate[] Rotate;		

		[XmlElement(ElementName = "attachment_full")]
		public IONET.Collada.Kinematics.Kinematics_Models.Attachment_Full Attachment_Full;	

		[XmlElement(ElementName = "attachment_end")]
		public IONET.Collada.Kinematics.Kinematics_Models.Attachment_End Attachment_End;	

		[XmlElement(ElementName = "attachment_start")]
		public IONET.Collada.Kinematics.Kinematics_Models.Attachment_Start Attachment_Start;	
	}
}

