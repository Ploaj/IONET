using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Physics_Model
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="rigid_body", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Rigid_Body
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("sid")]
		public string sID;

		[XmlAttribute("name")]
		public string Name;	
		
		
		[XmlElement(ElementName = "technique_common")]
		public IONET.Collada.Physics.Technique_Common.Technique_Common_Rigid_Body Technique_Common;
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;			
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

