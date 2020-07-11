using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Camera
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Orthographic
	{

		[XmlElement(ElementName = "xmag")]
		public IONET.Collada.Types.SID_Float XMag;				
				
		[XmlElement(ElementName = "ymag")]
		public IONET.Collada.Types.SID_Float YMag;				

		[XmlElement(ElementName = "aspect_ratio")]
		public IONET.Collada.Types.SID_Float Aspect_Ratio;				

		[XmlElement(ElementName = "znear")]
		public IONET.Collada.Types.SID_Float ZNear;				

		[XmlElement(ElementName = "zfar")]
		public IONET.Collada.Types.SID_Float ZFar;				
		
	}
}

