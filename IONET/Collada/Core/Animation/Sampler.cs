using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Animation
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Sampler
	{
		[XmlAttribute("id")]
		public string ID;
	
		[XmlAttribute("pre_behavior")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Sampler_Behavior.UNDEFINED)]
		public IONET.Collada.Enums.Sampler_Behavior Pre_Behavior;

		[XmlAttribute("post_behavior")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Sampler_Behavior.UNDEFINED)]
		public IONET.Collada.Enums.Sampler_Behavior Post_Behavior;
		
		
		[XmlElement(ElementName = "input")]
		public IONET.Collada.Core.Data_Flow.Input_Unshared[] Input;			
	}
}

