using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Rendering
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="pass", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Pass
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlElement(ElementName = "annotate")]
		public IONET.Collada.FX.Effects.Annotate[] Annotate;		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
		
	    [XmlElement(ElementName = "states")]
		public IONET.Collada.FX.Rendering.States States;		
		
	    [XmlElement(ElementName = "evaluate")]
		public IONET.Collada.FX.Effects.Effect_Technique Evaluate;		
	}
}

