using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Mathematics
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Formula
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;		

		[XmlAttribute("sid")]
		public string sID;
	
		
	    [XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;
	    
		[XmlElement(ElementName = "technique_common")]
		public IONET.Collada.Core.Technique_Common.Technique_Common_Formula Technique_Common;
	    
		[XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;		
		
		
		[XmlElement(ElementName = "target")]
		public IONET.Collada.Types.Common_Float_Or_Param_Type Target;		
		
	}
}

