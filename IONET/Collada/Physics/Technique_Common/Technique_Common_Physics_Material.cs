using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Technique_Common_Physics_Material : IONET.Collada.Core.Extensibility.Technique_Common
	{
		
		[XmlElement(ElementName = "dynamic_friction")]
		public IONET.Collada.Types.SID_Float Dynamic_Friction;	
		
		[XmlElement(ElementName = "restitution")]
		public IONET.Collada.Types.SID_Float Restitution;	
		
		[XmlElement(ElementName = "static_friction")]
		public IONET.Collada.Types.SID_Float Static_Friction;			
	}
}

