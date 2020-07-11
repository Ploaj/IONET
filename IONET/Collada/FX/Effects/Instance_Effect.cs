using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Effects
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="instance_effect", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Instance_Effect
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;		
		
		[XmlAttribute("url")]
		public string URL;		

		[XmlElement(ElementName = "setparam")]
		public IONET.Collada.Core.Parameters.Set_Param[] Set_Param;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
		
	    [XmlElement(ElementName = "technique_hint")]
		public IONET.Collada.FX.Effects.Technique_Hint[] Technique_Hint;			
		
	}
}

