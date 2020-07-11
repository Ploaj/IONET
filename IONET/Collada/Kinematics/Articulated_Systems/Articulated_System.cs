using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Articulated_Systems
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="articulated_system", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Articulated_System
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
		[XmlElement(ElementName = "kinematics")]
		public IONET.Collada.Kinematics.Articulated_Systems.Kinematics Kinematics;

		[XmlElement(ElementName = "motion")]
		public IONET.Collada.Kinematics.Articulated_Systems.Motion Motion;
	    
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

