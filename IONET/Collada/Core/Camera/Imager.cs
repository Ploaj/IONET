using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Camera
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Imager
	{
		
		
	    [XmlElement(ElementName = "technique")]
		public IONET.Collada.Core.Extensibility.Technique[] Technique;	
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
	}
}

