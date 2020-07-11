using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Physics_Model
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="ref_attachment", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Ref_Attachment
	{
		[XmlAttribute("rigid_body")]
		public string Rigid_Body;
		
		[XmlElement(ElementName = "translate")]
		public IONET.Collada.Core.Transform.Translate[] Translate;

		[XmlElement(ElementName = "rotate")]
		public IONET.Collada.Core.Transform.Rotate[] Rotate;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

