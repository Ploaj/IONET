using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Kinematics_Models
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="kinematics_model", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Kinematics_Model
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
		[XmlElement(ElementName = "technique_common")]
		public IONET.Collada.Kinematics.Technique_Common.Technique_Common_Kinematics_Model Technique_Common;
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;			
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra; 
	}
}

