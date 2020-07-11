using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Physics_Model
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="rigid_constraint", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Rigid_Constraint
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;	

		
		[XmlElement(ElementName = "ref_attachment")]
		public IONET.Collada.Physics.Physics_Model.Ref_Attachment Ref_Attachment;
		
		[XmlElement(ElementName = "attachment")]
		public IONET.Collada.Physics.Physics_Model.Attachment Attachment;
		
		
		[XmlElement(ElementName = "technique_common")]
		public IONET.Collada.Physics.Technique_Common.Technique_Common_Rigid_Constraint Technique_Common;
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;			
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

