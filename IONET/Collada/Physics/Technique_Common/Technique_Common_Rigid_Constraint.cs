using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Technique_Common_Rigid_Constraint : IONET.Collada.Core.Extensibility.Technique_Common
	{
		
		[XmlElement(ElementName = "enabled")]
		public IONET.Collada.Types.SID_Bool Enabled;
		
		[XmlElement(ElementName = "interpenetrate")]
		public IONET.Collada.Types.SID_Bool Interpenetrate;		
		
		[XmlElement(ElementName = "limits")]
		public IONET.Collada.Physics.Custom_Types.Constraint_Limits Limits;		
		
		
		[XmlElement(ElementName = "spring")]
		public IONET.Collada.Physics.Custom_Types.Constraint_Spring Spring;		
		
		
	}
}

