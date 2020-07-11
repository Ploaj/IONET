using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.B_Rep.Surfaces
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Surfaces
	{
	    [XmlElement(ElementName = "surface")]
		public IONET.Collada.B_Rep.Surfaces.Surface[] Surface;		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

