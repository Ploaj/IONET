using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Technique_Common_Optics : IONET.Collada.Core.Extensibility.Technique_Common
	{
		
		[XmlElement(ElementName = "orthographic")]
		public IONET.Collada.Core.Camera.Orthographic Orthographic;

		[XmlElement(ElementName = "perspective")]
		public IONET.Collada.Core.Camera.Perspective Perspective;	
	}
}

