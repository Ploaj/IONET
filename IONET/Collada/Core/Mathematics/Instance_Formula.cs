using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Mathematics
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Instance_Formula
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		[XmlAttribute("url")]
		public string URL;		
		
		
	    [XmlElement(ElementName = "setparam")]
		public IONET.Collada.Core.Parameters.Set_Param[] Set_Param;					
	}
}

