using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Physics_Model
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="instance_rigid_constraint", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Instance_Rigid_Constraint
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		[XmlAttribute("constraint")]
		public string Constraint;	
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

