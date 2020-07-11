using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Animation
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Animation_Clip
	{

		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		
		[XmlAttribute("start")]
		public double Start;	

		[XmlAttribute("end")]
		public double End;	
		
		
	    [XmlElement(ElementName = "instance_animation")]
		public IONET.Collada.Core.Animation.Instance_Animation[] Instance_Animation;	
		
	    [XmlElement(ElementName = "instance_formula")]
		public IONET.Collada.Core.Mathematics.Instance_Formula[] Instance_Formula;	
		

		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
	}
}

